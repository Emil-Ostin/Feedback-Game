using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // configurable parameters
    [Header("Enemy Stats")]
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] int damage;

    [Header("Ground & Ledge Detection")]
    [SerializeField] Transform ledgeCheckPosition;
    [SerializeField] float ledgeCheckLength;
    [SerializeField] Vector2 groundCheckPosition;
    [SerializeField] Vector2 groundCheckSize;
    [SerializeField] LayerMask groundLayer;

    // private variables
    bool isFacingRight;

    // cached references
    Rigidbody2D enemyRigidbody;

    HealthController health;

    void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        health = FindAnyObjectByType<HealthController>();
    }

    void FixedUpdate()
    {
        Move();
        LedgeCheck();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            health.Health(damage);
        }
    }

    void Move()
    {
        if (CheckGrounded())
        {
            enemyRigidbody.linearVelocityX = transform.right.x * moveSpeed;
        }
        else
        {
            enemyRigidbody.linearVelocityX = 0f;
        }
    }

    void LedgeCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            ledgeCheckPosition.position,
            Vector2.down,
            ledgeCheckLength,
            groundLayer);

        if (hit.collider == null && CheckGrounded())
        {
            isFacingRight = !isFacingRight;

            if (isFacingRight)
            {
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {
                transform.eulerAngles = new Vector3(0f, -180f, 0f);
            }
        }
    }

    bool CheckGrounded()
    {
        Collider2D isGrounded = Physics2D.OverlapBox(transform.position + (Vector3)groundCheckPosition, groundCheckSize, 0);

        return isGrounded;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + (Vector3)groundCheckPosition, groundCheckSize);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(ledgeCheckPosition.position, new Vector3(ledgeCheckPosition.position.x, ledgeCheckPosition.position.y - ledgeCheckLength));
    }
}
