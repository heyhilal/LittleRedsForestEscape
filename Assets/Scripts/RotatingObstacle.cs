using UnityEngine;

public class RotatingObstacle : MonoBehaviour
{
    public float rotationSpeed = 60f;

    void Update()
    {
      transform.Rotate(rotationSpeed * Time.deltaTime, 0f, 0f);
    }
}