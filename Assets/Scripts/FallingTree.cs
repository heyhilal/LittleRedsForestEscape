using UnityEngine;

public class FallingTree : MonoBehaviour
{
    public Transform treePivot;
    public GameObject treeBlockCollider;

    public float fallSpeed = 180f;
    public float targetAngle = 90f;

    private bool shouldFall = false;
    private float currentAngle = 0f;

    void Update()
    {
        if (shouldFall && currentAngle < targetAngle)
        {
            float step = fallSpeed * Time.deltaTime;
            treePivot.Rotate(Vector3.forward * step);
            currentAngle += step;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shouldFall = true;
        }
    }
}