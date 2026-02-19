using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;


public class BulletScript : MonoBehaviour
{
  [SerializeField] public float BulletSpeed = 20f;
  [SerializeField]  public float BulletDamage = 5f;
    public Rigidbody2D BulletRb;
    float nextHit;
    [SerializeField] float bulletLife;

    HealthController health;

    public void Start()
    {
        health = FindAnyObjectByType<HealthController>();
        BulletRb = GetComponent<Rigidbody2D>();
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        health.Health((int)BulletDamage);
    }
    private void FixedUpdate()
    {
        BulletRb.linearVelocity = transform.right * BulletSpeed;


        if (Time.time > bulletLife)
        {
            Destroy(gameObject);
            //nextHit = Time.time + bulletLife;
        }
    }

    
    
 
}



