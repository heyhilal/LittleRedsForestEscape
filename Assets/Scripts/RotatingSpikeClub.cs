using UnityEngine;

public class RotatingSpikeClub : MonoBehaviour
{
    [Header("Rotation")]
    public float rotationSpeed = 150f;

    [Header("Movement")]
    public float moveDistance = 2f;   // sağ-sol mesafe
    public float moveSpeed = 2f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // 🔄 DÖNME
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        // ↔️ SAĞ-SOL HAREKET
        float movement = Mathf.Sin(Time.time * moveSpeed) * moveDistance;

        transform.position = startPosition + new Vector3(0f, 0f, movement);
    }
}