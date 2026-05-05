using UnityEngine;

public class WolfChase : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float detectionDistance = 8f;
    public float catchDistance = 1.2f;

    [Header("Damage Settings")]
    public int damageAmount = 1;
    public float damageCooldown = 1.5f;
    public float pushBackForce = 4f;

    public string runAnimationName = "Running_New";
    public string idleAnimationName = "Idle_New";

    private Transform player;
    private bool isChasing = false;
    private bool canDamage = true;
    private Animator animator;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
            player = playerObj.transform;

        animator = GetComponent<Animator>();

        if (animator != null)
            animator.Play(idleAnimationName);
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < detectionDistance)
        {
            isChasing = true;
        }

        if (isChasing && distance > catchDistance)
        {
            Vector3 direction = player.position - transform.position;
            direction.y = 0f;

            if (direction.magnitude > 0.01f)
            {
                direction.Normalize();

                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.LookRotation(direction),
                    6f * Time.deltaTime
                );

                transform.position += direction * moveSpeed * Time.deltaTime;
            }

            if (animator != null &&
                !animator.GetCurrentAnimatorStateInfo(0).IsName(runAnimationName))
            {
                animator.Play(runAnimationName);
            }
        }
        else if (isChasing && distance <= catchDistance)
        {
            DamagePlayer();

            if (animator != null &&
                !animator.GetCurrentAnimatorStateInfo(0).IsName(idleAnimationName))
            {
                animator.Play(idleAnimationName);
            }
        }
        else
        {
            if (animator != null &&
                !animator.GetCurrentAnimatorStateInfo(0).IsName(idleAnimationName))
            {
                animator.Play(idleAnimationName);
            }
        }
    }

    void DamagePlayer()
    {
        if (!canDamage || player == null) return;

        canDamage = false;

        PlayerHealth health = player.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.TakeDamage(damageAmount);
        }

        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 pushDirection = (player.position - transform.position).normalized;
            pushDirection.y = 0f;

            rb.AddForce(pushDirection * pushBackForce, ForceMode.Impulse);
        }

        Invoke(nameof(ResetDamage), damageCooldown);
    }

    void ResetDamage()
    {
        canDamage = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DamagePlayer();
        }
    }
}