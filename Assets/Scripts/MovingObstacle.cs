using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public float moveDistance = 400f;
    public float moveSpeed = 2f;

    public bool moveOnZAxis = true;

    // 🔥 Yeni eklediğimiz
    public bool startFromA = false;

    private Vector3 pointA;
    private Vector3 pointB;
    private Vector3 targetPoint;

    void Start()
    {
        if (moveOnZAxis)
        {
            pointA = transform.position + new Vector3(0f, 0f, -moveDistance);
            pointB = transform.position + new Vector3(0f, 0f, moveDistance);
        }
        else
        {
            pointA = transform.position + new Vector3(-moveDistance, 0f, 0f);
            pointB = transform.position + new Vector3(moveDistance, 0f, 0f);
        }

        // 🔥 Başlangıç yönü seçimi
        targetPoint = startFromA ? pointA : pointB;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPoint,
            moveSpeed * Time.fixedDeltaTime
        );

        if (Vector3.Distance(transform.position, targetPoint) < 0.05f)
        {
            targetPoint = targetPoint == pointA ? pointB : pointA;
        }
    }
}