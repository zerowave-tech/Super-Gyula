using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apper : MonoBehaviour
{
    public AudioSource soundSource;
    public AudioClip soundClip;
    public GameObject objectToAppear;

    private void Start()
    {
        if (objectToAppear != null)
        {
            objectToAppear.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Простая проверка только по тегу
        if (other.CompareTag("Player"))
        {
            // Воспроизводим звук
            if (soundSource != null && soundClip != null)
            {
                soundSource.PlayOneShot(soundClip);
            }

            // Делаем объект видимым
            if (objectToAppear != null)
            {
                objectToAppear.SetActive(true);
            }

            // Опционально: отключаем триггер после использования
            // GetComponent<Collider2D>().enabled = false;
        }
    }
}