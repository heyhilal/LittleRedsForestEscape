using UnityEngine;
using System.Collections;

public class FallingBranch : MonoBehaviour
{
    public Transform branch;

    public Vector3 startPosition;
    public Vector3 fallPosition;

    public Vector3 startRotation;
    public Vector3 fallRotation;

    public float fallSpeed = 60f;
    public float rotateSpeed = 200f;

    private bool hasFallen = false;

    void Start()
    {
        branch.position = startPosition;
        branch.rotation = Quaternion.Euler(startRotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasFallen)
        {
            hasFallen = true;
            StartCoroutine(FallBranch());
        }
    }

    IEnumerator FallBranch()
    {
        Quaternion targetRot = Quaternion.Euler(fallRotation);

        while (Vector3.Distance(branch.position, fallPosition) > 0.05f ||
               Quaternion.Angle(branch.rotation, targetRot) > 1f)
        {
            branch.position = Vector3.MoveTowards(
                branch.position,
                fallPosition,
                fallSpeed * Time.deltaTime
            );

            branch.rotation = Quaternion.RotateTowards(
                branch.rotation,
                targetRot,
                rotateSpeed * Time.deltaTime
            );

            yield return null;
        }

        branch.position = fallPosition;
        branch.rotation = targetRot;
    }
}