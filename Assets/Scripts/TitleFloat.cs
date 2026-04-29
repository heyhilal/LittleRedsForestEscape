using UnityEngine;

public class TitleFloat : MonoBehaviour
{
    public float speed = 1f;
    public float height = 10f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        transform.localPosition = startPos + Vector3.up * Mathf.Sin(Time.time * speed) * height;
    }
}