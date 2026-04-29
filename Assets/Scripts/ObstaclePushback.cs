using UnityEngine;
using System.Collections;

public class ObstaclePushback : MonoBehaviour
{
    public float pushBackDistance = 2f;
    public float stopTime = 0.2f;

    private void OnCollisionEnter(Collision collision)
    {
        PlayerMovement movement = collision.collider.GetComponentInParent<PlayerMovement>();
        Rigidbody playerRb = collision.collider.GetComponentInParent<Rigidbody>();

        if (movement != null && playerRb != null)
        {
            StartCoroutine(PushBackPlayer(movement, playerRb));
        }
    }

    IEnumerator PushBackPlayer(PlayerMovement movement, Rigidbody playerRb)
    {
        movement.canMove = false;

        Vector3 direction = playerRb.transform.position - transform.position;
        direction.y = 0f;
        direction.Normalize();

        playerRb.linearVelocity = Vector3.zero;
        playerRb.MovePosition(playerRb.position + direction * pushBackDistance);

        yield return new WaitForSeconds(stopTime);

        movement.canMove = true;
    }
}