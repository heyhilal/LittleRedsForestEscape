using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int damage = 1;
    public Transform respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        DamagePlayer(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        DamagePlayer(collision.collider);
    }

    void DamagePlayer(Collider other)
    {
        PlayerHealth health = other.GetComponentInParent<PlayerHealth>();

        if (health != null)
        {
            health.TakeDamage(damage);

            if (respawnPoint != null)
            {
                Rigidbody rb = other.GetComponentInParent<Rigidbody>();

                if (rb != null)
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                    rb.position = respawnPoint.position;
                }
                else
                {
                    other.transform.position = respawnPoint.position;
                }
            }
        }
    }
}