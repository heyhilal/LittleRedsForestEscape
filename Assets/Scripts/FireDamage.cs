using UnityEngine;
using System.Collections;

public class FireDamage : MonoBehaviour
{
    public int damageAmount = 1;
    public float damageCooldown = 1f;

    [Header("Smoke Effect")]
    public GameObject smokeEffectPrefab;
    public float smokeDuration = 0.6f;

    private bool hasBurned = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasBurned) return;

        if (other.CompareTag("Player"))
        {
            hasBurned = true;

            // 🔻 DAMAGE
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(damageAmount);
            }

            // 🔻 SMOKE EFFECT (ATEŞ HİZASI)
            if (smokeEffectPrefab != null)
            {
                Vector3 spawnPos = other.bounds.min + new Vector3(0f, 0.1f, 0f);

                GameObject smoke = Instantiate(
                    smokeEffectPrefab,
                    spawnPos,
                    Quaternion.identity
                );

                Destroy(smoke, smokeDuration);
            }

            // 🔻 RESPAWN
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.Respawn();
            }

            StartCoroutine(ResetFireDamage());
        }
    }

    private IEnumerator ResetFireDamage()
    {
        yield return new WaitForSeconds(damageCooldown);
        hasBurned = false;
    }
}