using UnityEngine;
using System.Collections;

public class WaterDamage : MonoBehaviour
{
    public Transform respawnPoint;

    public float splashTime = 1.2f;
    public float shakeAmount = 0.08f;
    public float shakeSpeed = 35f;
    public float sinkSpeed = 6f;

    private bool isRespawning = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isRespawning)
        {
            StartCoroutine(RespawnPlayer(other.gameObject));
        }
    }

    IEnumerator RespawnPlayer(GameObject player)
    {
        isRespawning = true;

        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        Rigidbody rb = player.GetComponent<Rigidbody>();
        PlayerHealth health = player.GetComponent<PlayerHealth>();
        Animator animator = player.GetComponentInChildren<Animator>();

        if (movement != null)
            movement.canMove = false;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.useGravity = false;
        }

        if (animator != null)
        {
            animator.CrossFade("TreadingWater", 0f);
        }

        if (health != null)
            health.TakeDamage(1);

        Vector3 startPos = player.transform.position;
        float timer = 0f;

        while (timer < splashTime)
        {
            float offsetX = Mathf.Sin(timer * shakeSpeed) * shakeAmount;
            float offsetZ = Mathf.Cos(timer * shakeSpeed) * shakeAmount;

            player.transform.position = startPos + new Vector3(offsetX, -timer * sinkSpeed, offsetZ);

            timer += Time.deltaTime;
            yield return null;
        }

        if (respawnPoint != null)
        {
            player.transform.position = respawnPoint.position;
            player.transform.rotation = respawnPoint.rotation;
        }

        if (animator != null)
        {
            animator.Play("Locomotion", 0, 0f);
            animator.SetFloat("Speed", 0f);
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.useGravity = true;
        }

        if (movement != null)
            movement.canMove = true;

        yield return new WaitForSeconds(0.5f);

        isRespawning = false;
    }
}