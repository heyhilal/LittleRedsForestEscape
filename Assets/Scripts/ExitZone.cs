using UnityEngine;

public class ExitZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.root.CompareTag("Player")) return;

        if (GameManager.instance == null) return;

        if (GameManager.instance.levelCompleted)
        {
            GameManager.instance.ShowNextLevelPanel();
        }
        else
        {
            GameManager.instance.ShowWarningMessage();
        }
    }
}