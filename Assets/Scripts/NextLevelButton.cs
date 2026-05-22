using UnityEngine;

public class NextLevelButton : MonoBehaviour
{
    public void GoToNextLevel()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.LoadNextLevel();
        }
    }
}