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

    [Header("Level2 Fix")]
    public bool lockCameraDirection = false;
    public Vector3 fixedDirection = new Vector3(0, 0, 1);

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition;

        if (followBehindTarget)
        {
            Vector3 followDirection;

            if (lockCameraDirection)
            {
                followDirection = fixedDirection.normalized;
            }
            else
            {
                followDirection = target.forward;
            }

            desiredPosition =
                target.position
                - followDirection * distance
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

        transform.LookAt(target.position + Vector3.up * lookHeight);
    }
}