using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelTimer : MonoBehaviour
{
    public TMP_Text timerText;
    public TMP_Text currentTimeText;
    public TMP_Text bestTimeText;

    private float elapsedTime;
    private bool timerRunning = true;

    void Update()
    {
        if (!timerRunning)
            return;

        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        timerText.text =
            minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    public void StopTimer()
    {
        timerRunning = false;

        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        string currentTime =
            minutes.ToString("00") + ":" + seconds.ToString("00");

        currentTimeText.text = "Time: " + currentTime;

        string levelName = SceneManager.GetActiveScene().name;

        float bestTime = PlayerPrefs.GetFloat(levelName + "_BestTime", 9999f);

        if (elapsedTime < bestTime)
        {
            bestTime = elapsedTime;

            PlayerPrefs.SetFloat(levelName + "_BestTime", bestTime);
        }

        int bestMinutes = Mathf.FloorToInt(bestTime / 60);
        int bestSeconds = Mathf.FloorToInt(bestTime % 60);

        bestTimeText.text =
            "Best Time: "
            + bestMinutes.ToString("00")
            + ":"
            + bestSeconds.ToString("00");
    }
}