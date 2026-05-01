using UnityEngine;

public class CameraFollow_Level3 : MonoBehaviour
{
    public Transform target;

    public float distance = 7f;
    public float height = 2.2f;
    public float lookHeight = 1.4f;

    public float sideOffset = 0f;
    public float smoothSpeed = 6f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition =
            target.position
            - target.forward * distance
            + Vector3.up * height
            + target.right * sideOffset;

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        Vector3 lookTarget = target.position + Vector3.up * lookHeight;
        transform.LookAt(lookTarget);
    }
}