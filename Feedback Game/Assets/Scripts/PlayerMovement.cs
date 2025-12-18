using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpForce = 12f;

    [SerializeField] float fallGravityMultiplier = 2.5f;
    [SerializeField] float lowJumpMultiplier = 2f;


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
        if (jumpAction.WasPressedThisFrame() && isGrounded)
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
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (mousePos.x > transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Simple ground detection
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
