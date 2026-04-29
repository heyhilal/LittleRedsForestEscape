using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public float moveAmount = 10f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float mouseX = Input.mousePosition.x / Screen.width - 1f;
        float mouseY = Input.mousePosition.y / Screen.height - 1f;

        Vector3 target = new Vector3(mouseX, mouseY, 0) * moveAmount;

        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            startPos + target,
            Time.deltaTime * 2f
        );
    }
}