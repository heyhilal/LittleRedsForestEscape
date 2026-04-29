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

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHearts();

        playerRenderers = GetComponentsInChildren<Renderer>();
    }

    public void TakeDamage(int damage)
    {
        if (isBlinking) return; // blink sırasında tekrar hasar almasın

        currentHealth -= damage;

        if (currentHealth < 0)
            currentHealth = 0;

        UpdateHearts();

        StartCoroutine(BlinkEffect());

        if (currentHealth <= 0)
        {
            GameOver();
        }
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

        isBlinking = false;
    }

    void SetPlayerVisible(bool visible)
    {
        foreach (Renderer rend in playerRenderers)
        {
            rend.enabled = visible;
        }
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
                hearts[i].color = fullColor;
            else
                hearts[i].color = emptyColor;
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over!");

        if (GameManager.instance != null)
        {
            GameManager.instance.ShowGameOver();
        }
    }
}