using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    [Header("Normal Offset Mode")]
    public Vector3 offset = new Vector3(0f, 2.5f, -5f);

    [Header("Behind Target Mode")]
    public bool followBehindTarget = false;
    public float distance = 35f;
    public float height = 14f;
    public float sideOffset = 0f;
    public float worldSideOffset = 0f;
    public float lookHeight = 3f;

    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition;

        if (followBehindTarget)
        {
            desiredPosition =
                target.position
                - target.forward * distance
                + Vector3.up * height
                + target.right * sideOffset
                + transform.right * worldSideOffset;
        }
        else
        {
            desiredPosition = target.position + offset;
        }

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

if (followBehindTarget)
{
    transform.LookAt(target.position + Vector3.up * lookHeight);
}
else
{
    transform.LookAt(target.position);
}
    }
}