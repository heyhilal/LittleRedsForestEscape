using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int collectedCount = 0;
    public int totalCollectibles = 5;
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
        UpdateUI();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (levelCompletePanel != null)
            levelCompletePanel.SetActive(false);

        if (warningText != null)
            warningText.SetActive(false);
    }
private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
    if (crystalText == null)
    {
        GameObject scoreObj = GameObject.Find("ScoreText");

        if (scoreObj != null)
        {
            crystalText = scoreObj.GetComponent<TMP_Text>();
        }
    }

    UpdateUI();
}
    public void CollectItem()
    {
        collectedCount++;
        score += 15;   // her kristal +15 puan

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
} 