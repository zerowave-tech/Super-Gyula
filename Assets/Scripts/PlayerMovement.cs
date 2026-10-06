using System.Diagnostics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Camera mainCamera;
    private Rigidbody2D rb;
    private Collider2D capsuleCollider;

    private Vector2 velocity;
    private float inputAxis;

    // Добавляем переменные для кнопочного управления
    private float buttonInput;
    private bool jumpButtonPressed;
    private bool jumpButtonHeld; // Добавляем флаг удержания кнопки прыжка

    public float moveSpeed = 8f;
    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f;
    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow(maxJumpTime / 2f, 2f);

    public float slideFriction = 0.7f;

    public bool grounded { get; private set; }
    public bool jumping { get; private set; }
    public bool running => Mathf.Abs(velocity.x) > 0.25f || Mathf.Abs(inputAxis) > 0.25f;
    public bool sliding => (inputAxis > 0f && velocity.x < 0f) || (inputAxis < 0f && velocity.x > 0f);
    public bool falling => velocity.y < 0f && !grounded;

    private void Awake()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        rb.isKinematic = false;
        capsuleCollider.enabled = true;
        velocity = Vector2.zero;
        jumping = false;
        buttonInput = 0f;
        jumpButtonPressed = false;
        jumpButtonHeld = false;
    }

    private void OnDisable()
    {
        rb.isKinematic = true;
        capsuleCollider.enabled = false;
        velocity = Vector2.zero;
        inputAxis = 0f;
        buttonInput = 0f;
        jumping = false;
        jumpButtonPressed = false;
        jumpButtonHeld = false;
    }

    private void Update()
    {
        // Объединяем ввод с клавиатуры и кнопок
        inputAxis = Input.GetAxis("Horizontal") + buttonInput;
        inputAxis = Mathf.Clamp(inputAxis, -1f, 1f);

        // Обрабатываем нажатие прыжка с клавиатуры
        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonPressed = true;
            jumpButtonHeld = true;
        }

        // Обрабатываем отпускание прыжка с клавиатуры
        if (Input.GetButtonUp("Jump"))
        {
            jumpButtonHeld = false;
        }

        HorizontalMovement();

        grounded = rb.Raycast(Vector2.down);

        if (grounded)
        {
            GroundedMovement();
        }

        ApplyGravity();

        // Сбрасываем флаг нажатия после обработки
        jumpButtonPressed = false;
    }

    private void FixedUpdate()
    {
        Vector2 position = rb.position;
        position += velocity * Time.fixedDeltaTime;

        Vector2 leftEdge = mainCamera.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f);

        rb.MovePosition(position);
    }

    private void HorizontalMovement()
    {
        float acceleration = sliding ? moveSpeed * slideFriction : moveSpeed;
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, acceleration * Time.deltaTime);

        if (rb.Raycast(Vector2.right * velocity.x))
        {
            velocity.x = 0f;
        }

        if (velocity.x > 0f)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (velocity.x < 0f)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    private void GroundedMovement()
    {
        velocity.y = Mathf.Max(velocity.y, 0f);
        jumping = velocity.y > 0f;

        // Обрабатываем прыжок при нажатии (с клавиатуры ИЛИ с кнопки)
        if ((Input.GetButtonDown("Jump") || jumpButtonPressed) && !jumping)
        {
            velocity.y = jumpForce;
            jumping = true;
        }
    }

    private void ApplyGravity()
    {
        // Проверяем, отпущена ли кнопка прыжка (клавиша ИЛИ UI кнопка)
        bool jumpReleased = !Input.GetButton("Jump") && !jumpButtonHeld;
        bool falling = velocity.y < 0f || jumpReleased;
        float multiplier = falling ? 2f : 1f;

        velocity.y += gravity * multiplier * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, gravity / 2f);
    }

    // === МЕТОДЫ ДЛЯ КНОПОК ===

    public void StartMoveLeft()
    {
        buttonInput = -1f;
    }

    public void StartMoveRight()
    {
        buttonInput = 1f;
    }

    public void StopMove()
    {
        buttonInput = 0f;
    }

    public void StartJump()
    {
        jumpButtonPressed = true;
        jumpButtonHeld = true;
    }
    public void StopJump()
    {
        jumpButtonHeld = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (transform.DotTest(collision.transform, Vector2.down))
            {
                velocity.y = jumpForce / 2f;
                jumping = true;
            }
        }
        else if (collision.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
        {
            if (transform.DotTest(collision.transform, Vector2.up))
            {
                velocity.y = 0f;
            }
        }
    }
}