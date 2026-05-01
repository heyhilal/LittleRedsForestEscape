using UnityEngine;

public class Level2Finish : MonoBehaviour
{
    public GameObject levelCompletePanel;
    public GameObject warningText;

    [Header("Level 2 Requirement")]
    public int requiredLevel2Crystals = 4;

    private void Start()
    {
        if (levelCompletePanel != null)
            levelCompletePanel.SetActive(false);

        if (warningText != null)
            warningText.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        int level2Collected = GameManager.instance.collectedCount;

        if (level2Collected >= requiredLevel2Crystals)
        {
            if (levelCompletePanel != null)
                levelCompletePanel.SetActive(true);

            Time.timeScale = 0f;
        }
        else
        {
            if (warningText != null)
                warningText.SetActive(true);

            Debug.Log("Level2 kristalleri eksik: " + level2Collected + " / " + requiredLevel2Crystals);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (warningText != null)
            warningText.SetActive(false);
    }
}