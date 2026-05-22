using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pausePanel;

    void Start()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

 public void OpenPause()
{

    if (pausePanel == null)
    {
        GameObject foundPanel = GameObject.Find("PausePanel");
        if (foundPanel != null)
            pausePanel = foundPanel;
    }

    if (pausePanel != null)
    {
        pausePanel.transform.SetAsLastSibling();
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }
    else
    {
        Debug.LogWarning("PausePanel not found!");
    }
}

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}