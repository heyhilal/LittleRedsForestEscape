using UnityEngine;
using TMPro;
using System.Collections;

public class ExitZone : MonoBehaviour
{
    [Header("Crystal Requirement")]
    public int requiredCrystals = 4;

    [Header("Warning Text")]
    public TextMeshProUGUI warningText;

    [Header("Push Back")]
    public float pushBackForce = 2f;

    [Header("Warning Duration")]
    public float warningDuration = 2.5f;

    private void Start()
    {
        if (warningText != null)
            warningText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.root.CompareTag("Player")) return;

        if (GameManager.instance == null) return;

        if (GameManager.instance.collectedCount >= requiredCrystals)
        {
            GameManager.instance.levelCompleted = true;
            GameManager.instance.ShowNextLevelPanel();
        }
        else
        {
            int missing = requiredCrystals - GameManager.instance.collectedCount;

            if (warningText != null)
            {
                warningText.text = "Need " + missing + " more crystals!";
                warningText.gameObject.SetActive(true);
                warningText.transform.SetAsLastSibling();

                StopAllCoroutines();
                StartCoroutine(HideWarningAfterDelay());
            }
Transform player = other.transform.root;

Vector3 pushDirection = -player.forward;

player.position += pushDirection * pushBackForce;
        }
    }

    private IEnumerator HideWarningAfterDelay()
    {
        yield return new WaitForSeconds(warningDuration);

        if (warningText != null)
            warningText.gameObject.SetActive(false);
    }
}