using UnityEngine;

public class CrystalMovement : MonoBehaviour
{
    public float rotationSpeed = 180f;

    public float floatSpeed = 2f;
    public float floatHeight = 0.5f;

    public float scaleSpeed = 2f;
    public float scaleAmount = 0.15f;

    private Vector3 startPosition;
    private Vector3 startScale;

    private void Start()
    {
        startPosition = transform.position;
        startScale = transform.localScale;
    }

    private void Update()
    {
        // Daha belirgin dönme
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);

        // Yukarı-aşağı süzülme
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);

        // Hafif büyüyüp küçülme
        float scaleOffset = 1f + Mathf.Sin(Time.time * scaleSpeed) * scaleAmount;
        transform.localScale = startScale * scaleOffset;
    }
}