using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public DeathAnimation deathAnimation;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if (player != null)
            {
                // Проверяем, что игрок прыгнул сверху на врага
                if (player.transform.position.y > transform.position.y + 0.1f)
                {
                    // Игрок прыгнул на врага - враг умирает
                    Die();
                }
                else
                {
                    // Игрок столкнулся сбоку - игрок получает урон
                    player.Hit();
                }
            }
        }
    }

    public void Die()
    {
        // Запускаем анимацию смерти
        if (deathAnimation != null)
        {
            deathAnimation.enabled = true;
        }
        else
        {
            // Если нет анимации смерти, просто уничтожаем врага
            Destroy(gameObject);
        }

        // Отключаем движение врага
        if (TryGetComponent(out EntityMovement movement))
        {
            movement.enabled = false;
        }

        // Отключаем коллайдеры
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }
    }
}
