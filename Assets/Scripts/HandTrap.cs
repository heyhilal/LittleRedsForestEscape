using UnityEngine;
using System.Collections;

public class HandTrap : MonoBehaviour
{
    public Transform hand;

    public Vector3 hiddenPosition;
    public Vector3 blockPosition;

    public float moveSpeed = 80f;
    public float waitAtTop = 1f;
    public float waitAtBottom = 1f;

   void Start()
{
    hand.position = hiddenPosition;
    hand.gameObject.SetActive(false);
    StartCoroutine(HandLoop());
}

    IEnumerator HandLoop()
{
    while (true)
    {
        // görünür olsun
        hand.gameObject.SetActive(true);

        // dışarı çık
        while (Vector3.Distance(hand.position, blockPosition) > 0.05f)
        {
            hand.position = Vector3.MoveTowards(
                hand.position,
                blockPosition,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        yield return new WaitForSeconds(waitAtTop);

        while (Vector3.Distance(hand.position, hiddenPosition) > 0.05f)
{
    hand.position = Vector3.MoveTowards(
        hand.position,
        hiddenPosition,
        moveSpeed * Time.deltaTime
    );

    // kutuya yeterince yaklaştıysa direkt kaybol
    if (Vector3.Distance(hand.position, hiddenPosition) < 2f)
    {
        hand.gameObject.SetActive(false);
        break;
    }

    yield return null;
}

        // tamamen kaybolsun
        hand.gameObject.SetActive(false);

        yield return new WaitForSeconds(waitAtBottom);

        // tekrar çıkmadan önce pozisyon reset
        hand.position = hiddenPosition;
    }
}
}