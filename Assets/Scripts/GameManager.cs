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

    private void OnDestroy()
    {
        if (instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1f;

        collectedCount = 0;
        levelCompleted = false;

        gameOverPanel = null;
        levelCompletePanel = null;
        warningText = null;
        crystalText = null;

        FindSceneUIObjects();
        HidePanels();

        if (scene.name == "Level1")
        {
            totalCollectibles = 5;
        }
        else if (scene.name == "Level2")
        {
            totalCollectibles = 4;
        }
        else if (scene.name == "Level3")
        {
            totalCollectibles = 4; // Level3 kristal sayına göre değiştir
        }

        UpdateUI();
    }

    private void FindSceneUIObjects()
    {
        GameObject scoreObj = FindObjectEvenIfInactive("ScoreText");
        if (scoreObj != null)
            crystalText = scoreObj.GetComponent<TMP_Text>();

        gameOverPanel = FindObjectEvenIfInactive("GameOverPanel");
        levelCompletePanel = FindObjectEvenIfInactive("LevelCompletePanel");
        warningText = FindObjectEvenIfInactive("WarningText");

        if (gameOverPanel == null)
            Debug.LogWarning("GameOverPanel sahnede bulunamadı!");
        else
            Debug.Log("GameOverPanel bulundu: " + gameOverPanel.name);
    }

    private GameObject FindObjectEvenIfInactive(string objectName)
    {
        Transform[] allObjects = Resources.FindObjectsOfTypeAll<Transform>();

        foreach (Transform obj in allObjects)
        {
            if (obj.name == objectName && obj.gameObject.scene == SceneManager.GetActiveScene())
            {
                return obj.gameObject;
            }
        }

        return null;
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
        if (gameOverPanel == null)
        {
            gameOverPanel = FindObjectEvenIfInactive("GameOverPanel");
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            Debug.LogWarning("GameOverPanel bulunamadı!");
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;

        collectedCount = 0;
        score = 0;
        levelCompleted = false;

        gameOverPanel = null;
        levelCompletePanel = null;
        warningText = null;
        crystalText = null;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;

        collectedCount = 0;
        score = 0;
        levelCompleted = false;

        gameOverPanel = null;
        levelCompletePanel = null;
        warningText = null;
        crystalText = null;

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