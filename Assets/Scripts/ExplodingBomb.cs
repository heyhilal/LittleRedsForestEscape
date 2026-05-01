using UnityEngine;
using System.Collections;

public class ExplodingBomb : MonoBehaviour
{
    public int damageAmount = 1;

    [Header("Idle Shake")]
    public float shakeAmount = 0.1f;
    public float shakeSpeed = 5f;

    [Header("Fire Object In Scene")]
    public GameObject fireObject;
    public float fireDuration = 2f;

    private Vector3 startPosition;
    private bool exploded = false;

    void Start()
    {
        startPosition = transform.position;

        if (fireObject != null)
        {
            fireObject.SetActive(false);
        }
    }

    void Update()
    {
        if (exploded) return;

        float x = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;
        transform.position = startPosition + new Vector3(x, 0f, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (exploded) return;

        if (other.CompareTag("Player"))
        {
            exploded = true;

            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(damageAmount);
            }

            transform.position = startPosition;

            if (fireObject != null)
            {
                StartCoroutine(ShowFireTemporarily());
            }
        }
    }

    IEnumerator ShowFireTemporarily()
    {
        fireObject.SetActive(true);
        yield return new WaitForSeconds(fireDuration);
        fireObject.SetActive(false);
    }
}