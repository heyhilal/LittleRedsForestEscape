using UnityEngine;

public class GameOverButtonActions : MonoBehaviour
{
  public void RestartLevel()
{

    Time.timeScale = 1f;

    if (GameManager.instance != null)
        GameManager.instance.RestartLevel();
    else
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
}

    public void GoToMainMenu()
    {

        if (GameManager.instance != null)
            GameManager.instance.GoToMainMenu();
        else
            Debug.LogWarning("GameManager instance null!");
    }

    public void ExitGame()
    {
    
        if (GameManager.instance != null)
            GameManager.instance.ExitGame();
        else
            Debug.LogWarning("GameManager instance null!");
    }
}