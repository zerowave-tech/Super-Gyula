using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class прыжок : MonoBehaviour
{
    public AudioSource audiosource;
    public AudioClip clip;

    // Ссылка на скрипт движения игрока
    private PlayerMovement playerMovement;

    void Start()
    {
        // Находим скрипт PlayerMovement на этом же объекте
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        // Воспроизводим звук при нажатии пробела
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (audiosource != null && clip != null)
            {
                audiosource.PlayOneShot(clip);
            }
        }

        // Воспроизводим звук при вызове StartJump() из PlayerMovement
        if (playerMovement != null && IsJumpButtonPressed())
        {
            audiosource.PlayOneShot(clip);
        }
    }

    // Метод для проверки, была ли нажата кнопка прыжка
    private bool IsJumpButtonPressed()
    {

        return false;
    }
}