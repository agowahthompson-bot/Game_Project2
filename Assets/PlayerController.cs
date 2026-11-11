using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public sealed class PlayerController : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";
    private const string JumpButton = "Jump";
    private const float MovementThreshold = 0.1f;

    [Header("Movement Settings")]
    [SerializeField, Min(0f)] private float moveSpeed = 8f;
    [SerializeField, Min(0f)] private float jumpForce = 12f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField, Min(0f)] private float checkRadius = 0.2f;

    private Rigidbody2D rb;
    private float horizontalInput;
    private bool isGrounded;
    private bool jumpRequested;
    private PlayerState currentState;

    private enum PlayerState
    {
        Idle,
        Running,
        Jumping
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw(HorizontalAxis);
        isGrounded = groundCheck != null &&
                     Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer) != null;

        if (Input.GetButtonDown(JumpButton) && isGrounded)
        {
            jumpRequested = true;
        }

        UpdatePlayerState();
    }

    private void FixedUpdate()
    {
        Vector2 velocity = rb.linearVelocity;
        velocity.x = horizontalInput * moveSpeed;

        if (jumpRequested)
        {
            velocity.y = jumpForce;
            jumpRequested = false;
        }

        rb.linearVelocity = velocity;
    }

    private void UpdatePlayerState()
    {
        if (!isGrounded)
        {
            currentState = PlayerState.Jumping;
        }
        else if (Mathf.Abs(horizontalInput) > MovementThreshold)
        {
            currentState = PlayerState.Running;
        }
        else
        {
            currentState = PlayerState.Idle;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
        {
            return;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }
}
