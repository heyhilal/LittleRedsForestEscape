using UnityEngine;
using System.Collections;

public class BridgeFallRespawn : MonoBehaviour
{
    public Transform respawnPoint;

    [Header("Fall Settings")]
    public float fallDuration = 1.0f;
    public float fallSpeed = 35f;

    private bool isFalling = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (isFalling) return;

        StartCoroutine(FallAndRespawn(other.gameObject));
    }

    private IEnumerator FallAndRespawn(GameObject player)
    {
        isFalling = true;

        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        Rigidbody rb = player.GetComponent<Rigidbody>();
        PlayerHealth health = player.GetComponent<PlayerHealth>();
        Animator animator = player.GetComponent<Animator>();

        // Hareketi durdur
        if (movement != null)
            movement.enabled = false;

        // Can azalt
        if (health != null)
            health.TakeDamage(1);

        // Animasyona geç
        if (animator != null)
            animator.CrossFade("TreadingWater", 0.1f);

        // Fizik kontrolünü kapat
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.useGravity = false;
        }

        // Karakteri aşağı kaydır
        float timer = 0f;

        while (timer < fallDuration)
        {
            player.transform.position += Vector3.down * fallSpeed * Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }

        // Respawn noktasına ışınla
        if (respawnPoint != null)
        {
            player.transform.position = respawnPoint.position;
            player.transform.rotation = respawnPoint.rotation;
        }

        // Fiziği geri aç
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.useGravity = true;
        }

        // Normal animasyona dön
        if (animator != null)
            animator.CrossFade("Locomotion", 0.1f);

        // Hareketi geri aç
        if (movement != null)
            movement.enabled = true;

        isFalling = false;
    }
}