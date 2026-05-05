using UnityEngine;

public class Level3TurnTrigger : MonoBehaviour
{
    public bool isLeftTurnTrigger = false;
    public bool isRightTurnTrigger = false;

    [Header("Smooth Direction Settings")]
    public Vector3 leftMoveDirection = new Vector3(-1f, 0f, 1.5f);
    public Vector3 rightMoveDirection = new Vector3(1f, 0f, 1.5f);

    public bool useOnlyOnce = true;
    private bool used = false;

    private void OnTriggerEnter(Collider other)
    {
        if (useOnlyOnce && used) return;

        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();

            if (player != null)
            {
                if (isLeftTurnTrigger)
                {
                    player.SetLevel3MoveDirection(leftMoveDirection);
                    used = true;
                    Debug.Log("Level3 yumuşak sol dönüş çalıştı");
                }

                if (isRightTurnTrigger)
                {
                    player.SetLevel3MoveDirection(rightMoveDirection);
                    used = true;
                    Debug.Log("Level3 yumuşak sağ dönüş çalıştı");
                }
            }
        }
    }
}