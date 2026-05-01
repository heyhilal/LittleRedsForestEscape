using UnityEngine;

public class Level3TurnTrigger : MonoBehaviour
{
    public Transform exitPoint;
    public Vector3 newMoveDirection = Vector3.right;

    private bool used = false;

    private void OnTriggerEnter(Collider other)
    {
        if (used) return;

        if (other.CompareTag("Player"))
        {
            used = true;

            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                playerMovement.SetMoveDirection(newMoveDirection);
            }

            if (exitPoint != null)
            {
                other.transform.position = exitPoint.position;
            }

            gameObject.SetActive(false);
        }
    }
}