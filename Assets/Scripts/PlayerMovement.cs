using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 2.5f;
    public float runSpeed = 5f;
    public float jumpForce = 0.5f;
    public float forwardJumpForce = 1.5f;
    public float damageCooldown = 1f;
    public float strafeAmount = 0.5f;

    public bool useXAxisAsForward = false;
    public Transform respawnPoint;

    [Header("Level 1 Side Body Turn")]
    public bool enableSideBodyTurn = false;
    public float sideTurnAmount = 0.45f;
    public float sideTurnSpeed = 5f;

    [Header("Level 2 Landing Fix")]
    public bool snapToGroundOnLanding = false;

    [Header("Level 2 / Level 3 Turn System")]
    public bool enableTurnSystem = false;
    public Vector3 level2ForwardDirection = Vector3.left;
    public float turnSmoothSpeed = 1.5f;
    public float bodyTurnSpeed = 5f;

    private Vector3 targetForwardDirection;

    private Animator animator;
    private Rigidbody rb;
    private bool isGrounded = true;
    private bool canTakeDamage = true;
    private PlayerHealth playerHealth;

    private Vector3 moveInput;
    private float currentSpeed;
    public bool canMove = true;

    private bool canTurnLeft = false;
    private bool canTurnRight = false;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        playerHealth = GetComponent<PlayerHealth>();

        level2ForwardDirection = level2ForwardDirection.normalized;
        targetForwardDirection = level2ForwardDirection;

        rb.freezeRotation = true;
    }

    void Update()
    {
        if (!canMove)
        {
            moveInput = Vector3.zero;

            if (animator != null)
                animator.SetFloat("Speed", 0f);

            return;
        }

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (enableTurnSystem)
        {
            HandleLevel2Movement(h, v);

            if (canTurnLeft && h < -0.1f)
            {
                targetForwardDirection = (Vector3.left + Vector3.forward * 1f).normalized;
                canTurnLeft = false;
            }

            if (canTurnRight && h > 0.1f)
            {
                targetForwardDirection = (Vector3.right * 1.2f + Vector3.forward * 0.8f).normalized;
                canTurnRight = false;
            }
        }
        else
        {
            HandleNormalMovement(h, v);
        }

        bool isMoving = moveInput.magnitude > 0.1f;
        bool isRunning = isMoving &&
            (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));

        currentSpeed = isRunning ? runSpeed : walkSpeed;

        float blendValue = 0f;

        if (isMoving && !isRunning)
            blendValue = 0.5f;
        else if (isRunning)
            blendValue = 1f;

        if (animator != null)
            animator.SetFloat("Speed", blendValue);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void HandleLevel2Movement(float h, float v)
    {
        level2ForwardDirection = Vector3.Lerp(
            level2ForwardDirection,
            targetForwardDirection,
            turnSmoothSpeed * Time.deltaTime
        ).normalized;

        Vector3 forward = level2ForwardDirection.normalized;
        Vector3 right = Vector3.Cross(Vector3.up, forward).normalized;

        moveInput = forward * v + right * h * strafeAmount;

        if (moveInput.magnitude > 1f)
            moveInput.Normalize();

        Vector3 lookDirection = forward + right * h * 0.55f;

        if (lookDirection.magnitude < 0.1f)
            lookDirection = forward;

        if (Mathf.Abs(v) > 0.1f || Mathf.Abs(h) > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                bodyTurnSpeed * Time.deltaTime
            );
        }
    }

    void HandleNormalMovement(float h, float v)
    {
        if (useXAxisAsForward)
            moveInput = new Vector3(-v, 0f, -h * strafeAmount);
        else
            moveInput = new Vector3(h * strafeAmount, 0f, v);

        if (moveInput.magnitude > 1f)
            moveInput.Normalize();

        Vector3 baseForward = useXAxisAsForward ? Vector3.left : Vector3.forward;
        Vector3 baseBack = useXAxisAsForward ? Vector3.right : Vector3.back;
        Vector3 baseRight = useXAxisAsForward ? Vector3.back : Vector3.right;

        if (enableSideBodyTurn && Mathf.Abs(h) > 0.1f)
        {
            Vector3 lookDirection = baseForward + baseRight * h * sideTurnAmount;

            if (v < -0.1f)
                lookDirection = baseBack + baseRight * h * sideTurnAmount;

            Quaternion targetRotation = Quaternion.LookRotation(lookDirection.normalized);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                sideTurnSpeed * Time.deltaTime
            );
        }
        else
        {
            if (v > 0.1f)
                transform.forward = baseForward;
            else if (v < -0.1f)
                transform.forward = baseBack;
        }
    }

    void FixedUpdate()
    {
        if (!canMove)
            return;

        Vector3 targetPosition = rb.position + moveInput * currentSpeed * Time.fixedDeltaTime;
        rb.MovePosition(targetPosition);
    }

    void Jump()
    {
        Vector3 jumpDirection = transform.forward * forwardJumpForce;

        rb.linearVelocity = new Vector3(
            rb.linearVelocity.x,
            0f,
            rb.linearVelocity.z
        );

        rb.AddForce(
            new Vector3(jumpDirection.x, jumpForce, jumpDirection.z),
            ForceMode.Impulse
        );

        if (animator != null)
            animator.SetTrigger("JumpTrigger");

        isGrounded = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!enableTurnSystem)
            return;

        if (other.gameObject.name == "RightTurnTrigger")
            targetForwardDirection = (Vector3.left + Vector3.forward * 0.6f).normalized;

        if (other.gameObject.name == "RightTurnTrigger (2)")
            targetForwardDirection = (Vector3.left + Vector3.forward * 1.4f).normalized;

        if (other.gameObject.name == "StraightTrigger")
            targetForwardDirection = Vector3.forward;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;

            if (snapToGroundOnLanding)
            {
                rb.linearVelocity = new Vector3(
                    rb.linearVelocity.x,
                    0f,
                    rb.linearVelocity.z
                );
            }
        }

        if (collision.gameObject.CompareTag("Obstacle") && canTakeDamage)
        {
            if (playerHealth != null)
                playerHealth.TakeDamage(1);

            Vector3 pushDirection = -transform.forward;
            rb.MovePosition(rb.position + pushDirection * 2f);

            canTakeDamage = false;
            Invoke(nameof(ResetDamage), damageCooldown);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }

    void ResetDamage()
    {
        canTakeDamage = true;
    }

    public void SetLevel3MoveDirection(Vector3 newDirection)
    {
        enableTurnSystem = true;

        Vector3 currentDirection = transform.forward;
        currentDirection.y = 0f;
        currentDirection.Normalize();

        level2ForwardDirection = currentDirection;

        newDirection.y = 0f;
        newDirection.Normalize();

        targetForwardDirection = newDirection;

        Debug.Log("Yeni hedef yön: " + targetForwardDirection);
    }

    public void EnableLeftTurn()
    {
        enableTurnSystem = true;
        canTurnLeft = true;
    }

    public void EnableRightTurn()
    {
        enableTurnSystem = true;
        canTurnRight = true;
    }

    public void Respawn()
    {
        if (respawnPoint == null)
        {
            Debug.LogWarning("Respawn Point atanmadı!");
            return;
        }

        canMove = false;
        moveInput = Vector3.zero;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.position = respawnPoint.position;
        transform.rotation = respawnPoint.rotation;

        StartCoroutine(EnableMovement());
    }

    IEnumerator EnableMovement()
    {
        yield return new WaitForSeconds(0.2f);
        canMove = true;
    }
}