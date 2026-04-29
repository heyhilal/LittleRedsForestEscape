using UnityEngine;

public class UIButtonHover : MonoBehaviour
{
    public void OnEnter()
    {
        transform.localScale = Vector3.one * 1.1f;
    }

    public void OnExit()
    {
        transform.localScale = Vector3.one;
    }
}