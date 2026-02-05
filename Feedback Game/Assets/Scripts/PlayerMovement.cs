using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    ShootingScript shootingScript;

    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpForce = 12f;

    [SerializeField] float fallGravityMultiplier = 2.5f;
    [SerializeField] float lowJumpMultiplier = 2f;

    [SerializeField] Vector2 groundCheckPosition;
    [SerializeField] Vector2 groundCheckSize;

    Rigidbody2D rb;
    Camera cam;

    InputAction moveAction;
    InputAction jumpAction;

    bool isGrounded = true; // simple ground check

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
    }

    void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
    }

    void FixedUpdate()
    {
        MovePlayer();
        BetterJump();
    }

    void Update()
    {
        Jump();
        FlipPlayer();
    }

    void MovePlayer()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
    }

    void Jump()
    {
        if (jumpAction.WasPressedThisFrame() && CheckGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    void BetterJump()
    {
        if (rb.linearVelocity.y < 0)
        {
            // Falling
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallGravityMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.linearVelocity.y > 0 && !jumpAction.IsPressed())
        {
            // Jump button released early
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }


    void FlipPlayer()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, 10));

        if (mousePos.x > transform.position.x)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }

    bool CheckGrounded()
    {
        return Physics2D.OverlapBox(transform.position + (Vector3)groundCheckPosition, groundCheckSize, 0);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + (Vector3)groundCheckPosition, groundCheckSize);
    }
}
