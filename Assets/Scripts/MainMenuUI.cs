using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject settingsPanel;

    public void StartGame()
    {
        Time.timeScale = 1f;

        if (GameManager.instance != null)
        {
            GameManager.instance.collectedCount = 0;
            GameManager.instance.score = 0;
            GameManager.instance.levelCompleted = false;
        }

        SceneManager.LoadScene("Level1");
    }

    public void OpenSettings()
    {
        Time.timeScale = 1f;

        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}