using UnityEngine;

public class Level3SmallRightTurnTrigger : MonoBehaviour
{
    public Vector3 newMoveDirection = new Vector3(1f, 0f, 1.5f);

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

                Debug.Log("Level3 hafif sağ dönüş çalıştı");
            }
        }
    }
}