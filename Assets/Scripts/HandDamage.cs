using UnityEngine;

public class HandDamage : MonoBehaviour
{
    public Transform pushBackPoint;
    public float damageCooldown = 1f;

    private bool canDamage = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (!canDamage) return;

        PlayerHealth health = other.GetComponent<PlayerHealth>();

        if (health != null)
        {
            health.TakeDamage(1);
        }

        if (pushBackPoint != null)
{
    other.transform.position = pushBackPoint.position;
    other.transform.rotation = pushBackPoint.rotation;
}

        StartCoroutine(DamageCooldown());
    }

    private System.Collections.IEnumerator DamageCooldown()
    {
        canDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canDamage = true;
    }
}