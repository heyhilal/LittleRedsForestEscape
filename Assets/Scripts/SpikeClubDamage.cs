using UnityEngine;

public class SpikeClubDamage : MonoBehaviour
{
    public int damageAmount = 1;
    public float pushForce = 3f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // ❤️ DAMAGE
            PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(damageAmount);
            }

            // 💥 PUSHBACK
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 pushDirection = (collision.transform.position - transform.position).normalized;
                pushDirection.y = 0.3f; // hafif yukarı it

                rb.linearVelocity = Vector3.zero;

                rb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
            }
        }
    }
}