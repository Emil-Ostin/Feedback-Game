using UnityEngine;
using static UnityEngine.Rendering.DebugUI;


public class BulletScript : MonoBehaviour
{
    [Header("Gun Stats")]
    [SerializeField] public float BulletSpeed = 20f;
    [SerializeField] public float BulletDamage = 5f;
    [SerializeField] float bulletLifetime = 1.5f;

    [SerializeField] ParticleSystem hitVFXPrefab;

    public Rigidbody2D BulletRb;
    float nextHit;

    HealthController health;

    public void Start()
    {
        health = FindAnyObjectByType<HealthController>();
        BulletRb = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        Invoke("Destroy", bulletLifetime);
    }
    private void Destroy() { Destroy(gameObject); }

    private void FixedUpdate()
    {
        BulletRb.linearVelocity = transform.right * BulletSpeed;
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        health.Health((int)BulletDamage);
        Instantiate(hitVFXPrefab, collision.transform.position, Quaternion.identity);
        Debug.Log(collision);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(hitVFXPrefab, collision.transform.position, Quaternion.identity);
        Debug.Log(collision);
    }
}



