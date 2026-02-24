using UnityEditor.ShaderGraph;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // configurable parameters
    [Header("Enemy Stats")]
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] int damage;
    [SerializeField] float attackCD = 1f;
    [SerializeField] Vector2 hitboxPosA;
    [SerializeField] Vector2 hitboxPosB;
    [SerializeField] LayerMask playerLayer;
    float nextHit;

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

    HealthController otherHealth;

    HealthController myHealth;

    void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        myHealth = GetComponent<HealthController>();
    }

    void FixedUpdate()
    {
        if (myHealth.isDead == true) { return; }

        Move();
        LedgeCheck();

        OnAttack();
    }

    void OnAttack()
    {
        Collider2D hitbox = Physics2D.OverlapArea(transform.position + (Vector3)hitboxPosA, transform.position + (Vector3)hitboxPosB, playerLayer);

        if (hitbox && Time.time > nextHit)
        {
            otherHealth = hitbox.GetComponent<HealthController>();

            Debug.Log("Hit");
            otherHealth.Health(damage);

            nextHit = Time.time + attackCD;
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

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position + new Vector3(hitboxPosA.x, hitboxPosA.y, 0), transform.position + new Vector3(hitboxPosA.x, -hitboxPosA.y, 0));
        Gizmos.DrawLine(transform.position + new Vector3(hitboxPosB.x, hitboxPosB.y, 0), transform.position + new Vector3(hitboxPosA.x, -hitboxPosA.y, 0));
        Gizmos.DrawLine(transform.position + new Vector3(hitboxPosB.x, hitboxPosB.y, 0), transform.position + new Vector3(hitboxPosB.x, -hitboxPosB.y, 0));
        Gizmos.DrawLine(transform.position + new Vector3(hitboxPosA.x, hitboxPosA.y, 0), transform.position + new Vector3(hitboxPosB.x, -hitboxPosB.y, 0));
    }
}
