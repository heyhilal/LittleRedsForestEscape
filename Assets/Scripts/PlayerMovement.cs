using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 2.5f;
    public float runSpeed = 5f;
    public float jumpForce = 0.5f;
    public float forwardJumpForce = 1.5f;
    public float damageCooldown = 1f;
    public float strafeAmount = 0.5f;

    public bool useXAxisAsForward = false;

    [Header("Level 2 Landing Fix")]
    public bool snapToGroundOnLanding = false;

    [Header("Level 2 Turn System")]
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

    // W/S ileri geri, A/D yana kayma
    moveInput = forward * v + right * h * strafeAmount;

    if (moveInput.magnitude > 1f)
        moveInput.Normalize();

    // Karakter her zaman yol yönüne baksın
    Vector3 lookDirection = forward;

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

        if (v > 0.1f)
            transform.forward = useXAxisAsForward ? Vector3.left : Vector3.forward;
        else if (v < -0.1f)
            transform.forward = useXAxisAsForward ? Vector3.right : Vector3.back;
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

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

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
        {
            targetForwardDirection = (Vector3.left + Vector3.forward * 0.6f).normalized;
        }

        if (other.gameObject.name == "RightTurnTrigger (2)")
        {
            targetForwardDirection = (Vector3.left + Vector3.forward * 1.4f).normalized;
        }

        if (other.gameObject.name == "StraightTrigger")
{
    targetForwardDirection = Vector3.forward;
}
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

    // Karakteri çarptığı yerden biraz geri it
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

    public void SetMoveDirection(Vector3 newDirection)
{
    level2ForwardDirection = newDirection.normalized;
    enableTurnSystem = true;
}
}
