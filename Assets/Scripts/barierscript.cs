using UnityEngine;

public class barierscript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            player.Death();

        }
        if (other.CompareTag("Enemy"))
        {
            DeathAnimation deathAnimation = other.GetComponent<DeathAnimation>();
            deathAnimation.enabled = true;
        }
    }
}