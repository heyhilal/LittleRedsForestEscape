using UnityEngine;
using System.Collections;

public class TreeObstacleHit : MonoBehaviour
{
    public int damageAmount = 1;
    public float pushBackDistance = 8f;
    public float stopMoveTime = 0.5f;
    public float cooldown = 1f;

    private bool canHit = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!canHit)
            return;

        if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            PlayerMovement movement = other.GetComponent<PlayerMovement>();
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (health != null)
                health.TakeDamage(damageAmount);

            if (movement != null)
                movement.canMove = false;

            if (rb != null)
            {
          Vector3 pushDirection = other.transform.position - transform.position;
          pushDirection.y = 0f;
          pushDirection.Normalize();
                rb.linearVelocity = Vector3.zero;
                rb.position = rb.position + pushDirection * pushBackDistance;
            }

            StartCoroutine(ResetPlayer(movement));
        }
    }

    IEnumerator ResetPlayer(PlayerMovement movement)
    {
        canHit = false;

        yield return new WaitForSeconds(stopMoveTime);

        if (movement != null)
            movement.canMove = true;

        yield return new WaitForSeconds(cooldown);

        canHit = true;
    }
}