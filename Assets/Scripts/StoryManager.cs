using UnityEngine;
using TMPro;

public class StoryManager : MonoBehaviour
{
    public static bool hasStoryBeenShown = false;
    public static bool isStoryActive = false;

    public GameObject storyPanel;
    public TextMeshProUGUI storyText;

    public GameObject continueButton;
    public GameObject startButton;
    public GameObject skipButton;

    public PlayerMovement playerMovement;

    private int currentStoryIndex = 0;

    private string[] storyLines =
    {
        "Little Red was on her way to visit her grandmother.",
        "But the forest was no longer safe.",
        "A dark curse had awakened among the trees.",
        "The crystals were the only light left in the woods.",
        "Now, she must escape before the wolf finds her."
    };

    void Start()
    {
        if (hasStoryBeenShown)
        {
            storyPanel.SetActive(false);
            isStoryActive = false;

            if (playerMovement != null)
                playerMovement.canMove = true;

            Time.timeScale = 1f;
            return;
        }

        storyPanel.SetActive(false);

        if (playerMovement != null)
            playerMovement.canMove = false;

        isStoryActive = true;

        Invoke(nameof(ShowStory), 0.5f);
    }

    void ShowStory()
    {
        storyPanel.SetActive(true);

        currentStoryIndex = 0;
        storyText.text = storyLines[currentStoryIndex];

        continueButton.SetActive(true);
        skipButton.SetActive(true);
        startButton.SetActive(false);
    }

    public void NextStory()
    {
        currentStoryIndex++;

        if (currentStoryIndex < storyLines.Length)
        {
            storyText.text = storyLines[currentStoryIndex];
        }

        if (currentStoryIndex == storyLines.Length - 1)
        {
            continueButton.SetActive(false);
            startButton.SetActive(true);
        }
    }

    public void StartGame()
    {
        FinishStory();
    }

    public void SkipStory()
    {
        FinishStory();
    }

    private void FinishStory()
    {
        hasStoryBeenShown = true;
        isStoryActive = false;

        storyPanel.SetActive(false);

        if (playerMovement != null)
            playerMovement.canMove = true;

        Time.timeScale = 1f;
    }
}