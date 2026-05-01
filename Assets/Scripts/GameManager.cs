using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int collectedCount = 0;
    public int totalCollectibles = 4;
    public int score = 0;

    public TMP_Text crystalText;
    public GameObject gameOverPanel;
    public GameObject levelCompletePanel;
    public GameObject warningText;

    public bool levelCompleted = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        Time.timeScale = 1f;

        FindSceneUIObjects();
        HidePanels();
        UpdateUI();
    }

private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
    FindSceneUIObjects();
    HidePanels();

    collectedCount = 0;
    levelCompleted = false;

    if (scene.name == "Level1")
    {
        totalCollectibles = 5; // Level1 kristal sayın
    }
    else if (scene.name == "Level2")
    {
        totalCollectibles = 4; // Level2 kristal sayın
    }

    UpdateUI();
    Time.timeScale = 1f;
}

    private void FindSceneUIObjects()
    {
        GameObject scoreObj = GameObject.Find("ScoreText");
        if (scoreObj != null)
        {
            crystalText = scoreObj.GetComponent<TMP_Text>();
        }

        GameObject gameOverObj = GameObject.Find("GameOverPanel");
        if (gameOverObj != null)
        {
            gameOverPanel = gameOverObj;
        }

        GameObject levelCompleteObj = GameObject.Find("LevelCompletePanel");
        if (levelCompleteObj != null)
        {
            levelCompletePanel = levelCompleteObj;
        }

        GameObject warningObj = GameObject.Find("WarningText");
        if (warningObj != null)
        {
            warningText = warningObj;
        }
    }

    private void HidePanels()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (levelCompletePanel != null)
            levelCompletePanel.SetActive(false);

        if (warningText != null)
            warningText.SetActive(false);
    }

    public void CollectItem()
    {
        collectedCount++;
        score += 15;

        UpdateUI();

        if (collectedCount >= totalCollectibles)
        {
            LevelComplete();
        }
    }

    public void UpdateUI()
    {
        if (crystalText != null)
        {
            crystalText.text = "Score: " + score;
        }
    }

    void LevelComplete()
    {
        levelCompleted = true;
    }

    public void ShowNextLevelPanel()
    {
        if (levelCompleted && levelCompletePanel != null)
        {
            levelCompletePanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void ShowWarningMessage()
    {
        if (warningText != null)
        {
            StopAllCoroutines();
            StartCoroutine(ShowWarningRoutine());
        }
    }

    private IEnumerator ShowWarningRoutine()
    {
        warningText.SetActive(true);
        yield return new WaitForSeconds(2f);
        warningText.SetActive(false);
    }

 public void LoadNextLevel()
{
    if (!levelCompleted) return;

    Time.timeScale = 1f;

    collectedCount = 0;
    levelCompleted = false;

    SceneManager.LoadScene("Level2");
}

    public void ShowGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;

        collectedCount = 0;
        score = 0;
        levelCompleted = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;

        collectedCount = 0;
        score = 0;
        levelCompleted = false;

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