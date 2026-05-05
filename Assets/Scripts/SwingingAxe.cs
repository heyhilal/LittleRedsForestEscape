using UnityEngine;

public class SwingingAxe : MonoBehaviour
{
    public float moveAmount = 1.5f;
    public float moveSpeed = 2f;
    public bool reverseDirection = false;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float x = Mathf.Sin(Time.time * moveSpeed) * moveAmount;

        if (reverseDirection)
            x = -x;

        transform.localPosition = startPos + new Vector3(0f, 0f, x);
    }
}