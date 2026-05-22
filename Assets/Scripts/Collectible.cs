using UnityEngine;

public class Collectible : MonoBehaviour
{
    private bool collected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (collected) return;

        if (other.CompareTag("Player"))
        {
            collected = true;

            if (GameManager.instance != null)
            {
                GameManager.instance.CollectItem();
            }

            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayCrystalCollect();
            }

            gameObject.SetActive(false);
        }
    }
}