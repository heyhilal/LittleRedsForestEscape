using UnityEngine;

public class Level3SmallLeftTurnTrigger : MonoBehaviour
{
    public Vector3 newMoveDirection = new Vector3(-0.25f, 0f, 1f);

    private bool used = false;

    private void OnTriggerEnter(Collider other)
    {
        if (used) return;

        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();

            if (player != null)
            {
                player.SetLevel3MoveDirection(newMoveDirection);
                used = true;

                Debug.Log("Level3 küçük sola dönüş çalıştı");
            }
        }
    }
}