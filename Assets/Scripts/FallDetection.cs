using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDetection : MonoBehaviour
{
    public class FallDetectoin : MonoBehaviour
    {
        public string deathZoneTag = "DeathZone"; // Тег зоны смерти

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(deathZoneTag))
            {
                TriggerDeathAnimation();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(deathZoneTag))
            {
                TriggerDeathAnimation();
            }
        }

        private void TriggerDeathAnimation()
        {
            if (TryGetComponent(out DeathAnimation deathAnimation))
            {
                deathAnimation.enabled = true;
            }
        }
    }
}
