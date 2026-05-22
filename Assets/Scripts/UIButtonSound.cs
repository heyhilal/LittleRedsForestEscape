using UnityEngine;

public class UIButtonSound : MonoBehaviour
{
    public void PlayClickSound()
    {
        AudioManager.Instance.PlayButtonClick();
    }
}