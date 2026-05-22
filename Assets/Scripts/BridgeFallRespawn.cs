using UnityEngine;
using System.Collections;

public class BridgeFallRespawn : MonoBehaviour
{
    public Transform respawnPoint;

    [Header("Fall Settings")]
    public float fallDuration = 1.0f;
    public float fallSpeed = 35f;

    [Header("Sound Timing")]
    public float splashDelay = 0.25f;

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
        Animator animator = player.GetComponentInChildren<Animator>();

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayFallingScream();

        yield return new WaitForSeconds(splashDelay);

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayWaterSplash();

        if (movement != null)
            movement.enabled = false;

        if (health != null)
            health.TakeDamage(1);

        if (animator != null)
            animator.CrossFade("TreadingWater", 0.1f);

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.useGravity = false;
        }

        float timer = 0f;

        while (timer < fallDuration)
        {
            player.transform.position += Vector3.down * fallSpeed * Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }

        if (respawnPoint != null)
        {
            player.transform.position = respawnPoint.position;
            player.transform.rotation = respawnPoint.rotation;
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.useGravity = true;
        }

        if (animator != null)
            animator.CrossFade("Locomotion", 0.1f);

        if (movement != null)
            movement.enabled = true;

        isFalling = false;
    }
}