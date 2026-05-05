using UnityEngine;

public class WolfDamage : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // ❌ ARTIK DAMAGE YOK
            // ❌ PUSHBACK YOK

            // sadece debug için istersen:
            Debug.Log("Wolf touched player");
        }
    }
}