using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public Image[] hearts;

    public Color fullColor = Color.white;
    public Color emptyColor = Color.gray;

    private Renderer[] playerRenderers;
    private bool isBlinking = false;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        isDead = false;
        isBlinking = false;

        Time.timeScale = 1f;

        playerRenderers = GetComponentsInChildren<Renderer>();
        UpdateHearts();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;
        if (isBlinking) return;

        currentHealth -= damage;

        if (currentHealth < 0)
            currentHealth = 0;

        UpdateHearts();

        if (currentHealth <= 0)
        {
            GameOver();
            return;
        }

        StartCoroutine(BlinkEffect());
    }

    IEnumerator BlinkEffect()
    {
        isBlinking = true;

        for (int i = 0; i < 3; i++)
        {
            SetPlayerVisible(false);
            yield return new WaitForSeconds(0.15f);

            SetPlayerVisible(true);
            yield return new WaitForSeconds(0.15f);
        }

        SetPlayerVisible(true);
        isBlinking = false;
    }

    void SetPlayerVisible(bool visible)
    {
        if (playerRenderers == null) return;

        foreach (Renderer rend in playerRenderers)
        {
            if (rend != null)
                rend.enabled = visible;
        }
    }

    void UpdateHearts()
    {
        if (hearts == null) return;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] == null) continue;

            if (i < currentHealth)
                hearts[i].color = fullColor;
            else
                hearts[i].color = emptyColor;
        }
    }

    void GameOver()
    {
        isDead = true;
        isBlinking = false;

        SetPlayerVisible(true);

        Debug.Log("Game Over!");

        if (GameManager.instance != null)
        {
            GameManager.instance.ShowGameOver();
        }
        else
        {
            Debug.LogWarning("GameManager instance bulunamadı!");
        }
    }
}