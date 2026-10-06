using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FlagPole : MonoBehaviour
{
    public Transform flag;
    public Transform poleBottom;
    public Transform castle;
    public float speed = 6f;
    public string Level = "Main Menu";

    public Sprite flagLoweredSprite;
    public bool changeFlagSprite = true;

    [Header("Walking Animation Settings")]
    public Sprite[] walkingSprites; // Массив из двух спрайтов для анимации ходьбы
    public float walkAnimationSpeed = 0.2f; // Скорость смены спрайтов

    [Header("Scene Transition Settings")]
    public Image fadeImage;
    public float fadeDuration = 1f;
    public Color fadeColor = Color.black;

    private bool isWalkingAnimationPlaying = false;
    private Coroutine walkingCoroutine;

    private void Start()
    {
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out Player player))
        {
            StartCoroutine(MoveTo(flag, poleBottom.position, changeFlagSprite ? flagLoweredSprite : null));
            StartCoroutine(LevelCompleteSequence(player));
        }
    }

    private IEnumerator LevelCompleteSequence(Player player)
    {
        player.movement.enabled = false;

        SpriteRenderer playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        Sprite originalPlayerSprite = playerSpriteRenderer != null ? playerSpriteRenderer.sprite : null;

        yield return MoveTo(player.transform, poleBottom.position);

        // Начинаем анимацию ходьбы когда игрок движется к замку
        if (playerSpriteRenderer != null && walkingSprites != null && walkingSprites.Length >= 2)
        {
            StartWalkingAnimation(playerSpriteRenderer);
        }

        yield return MoveTo(player.transform, player.transform.position + Vector3.right);
        yield return MoveTo(player.transform, player.transform.position + Vector3.right + Vector3.down);
        yield return MoveTo(player.transform, castle.position);

        // Останавливаем анимацию ходьбы
        StopWalkingAnimation();

        // Возвращаем оригинальный спрайт если нужно
        if (playerSpriteRenderer != null && originalPlayerSprite != null)
        {
            playerSpriteRenderer.sprite = originalPlayerSprite;
        }

        player.gameObject.SetActive(false);

        yield return StartCoroutine(FadeTransitionEffect());

        SceneManager.LoadScene(1);
    }

    private IEnumerator MoveTo(Transform subject, Vector3 position, Sprite newSprite = null)
    {
        SpriteRenderer spriteRenderer = subject.GetComponent<SpriteRenderer>();
        Sprite originalSprite = null;

        if (spriteRenderer != null && newSprite != null)
        {
            originalSprite = spriteRenderer.sprite;
            spriteRenderer.sprite = newSprite;
        }

        while (Vector3.Distance(subject.position, position) > 0.125f)
        {
            subject.position = Vector3.MoveTowards(subject.position, position, speed * Time.deltaTime);
            yield return null;
        }

        subject.position = position;

        if (spriteRenderer != null && originalSprite != null)
        {
            spriteRenderer.sprite = originalSprite;
        }
    }

    // Корутина для анимации ходьбы
    private void StartWalkingAnimation(SpriteRenderer spriteRenderer)
    {
        if (isWalkingAnimationPlaying) return;

        isWalkingAnimationPlaying = true;
        walkingCoroutine = StartCoroutine(WalkingAnimationRoutine(spriteRenderer));
    }

    private void StopWalkingAnimation()
    {
        if (!isWalkingAnimationPlaying) return;

        isWalkingAnimationPlaying = false;
        if (walkingCoroutine != null)
        {
            StopCoroutine(walkingCoroutine);
            walkingCoroutine = null;
        }
    }

    private IEnumerator WalkingAnimationRoutine(SpriteRenderer spriteRenderer)
    {
        int currentSpriteIndex = 0;

        while (isWalkingAnimationPlaying)
        {
            if (walkingSprites != null && walkingSprites.Length >= 2)
            {
                // Переключаемся между двумя спрайтами
                spriteRenderer.sprite = walkingSprites[currentSpriteIndex];
                currentSpriteIndex = (currentSpriteIndex + 1) % 2; // Чередуем 0 и 1
            }

            yield return new WaitForSeconds(walkAnimationSpeed);
        }
    }

    private IEnumerator FadeTransitionEffect()
    {
        if (fadeImage == null)
        {
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(1);
            yield break;
        }

        fadeImage.gameObject.SetActive(true);
        fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 0f);

        // Затемнение
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha);
            yield return null;
        }

        fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 1f);

        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(1);
    }

    public void CompleteLevelByTimer(Player player)
    {
        StartCoroutine(TimerCompleteSequence(player));
    }

    private IEnumerator TimerCompleteSequence(Player player)
    {

        if (player != null)
        {
            player.movement.enabled = false;
            StopWalkingAnimation();
        }

        yield return StartCoroutine(FadeTransitionEffect());

        if (player != null)
        {
            player.gameObject.SetActive(false);
        }

        SceneManager.LoadScene(1);
    }
}