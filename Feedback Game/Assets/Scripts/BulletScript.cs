using UnityEngine;
using static UnityEngine.Rendering.DebugUI;


public class BulletScript : MonoBehaviour
{
    [Header("Gun Stats")]
    [SerializeField] public float bulletSpeed = 20f;
    [SerializeField] public float bulletDamage = 5f;
    [SerializeField] float bulletLifetime = 1.5f;

    [SerializeField] ParticleSystem hitVFXPrefab;
    [SerializeField] ParticleSystem MuzzlleFlashVFXPrefab;

    public Rigidbody2D BulletRb;
    float nextHit;

    HealthController health;

    public void Start()
    {
        
        BulletRb = GetComponent<Rigidbody2D>();

        Instantiate(MuzzlleFlashVFXPrefab, transform.position, Quaternion.identity);
    }

    private void Awake()
    {
        Invoke("Destroy", bulletLifetime);
    }
    private void Destroy() { Destroy(gameObject); }

    private void FixedUpdate()
    {
        BulletRb.linearVelocity = transform.right * bulletSpeed;
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        health = collision.gameObject.GetComponent<HealthController>();

        health.Health((int)bulletDamage);
        //Instantiate(hitVFXPrefab, transform.position, Quaternion.identity);
        GameObject.Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(hitVFXPrefab, transform.position, Quaternion.identity);
        Debug.Log(collision);
        GameObject.Destroy(gameObject);
    }
}



