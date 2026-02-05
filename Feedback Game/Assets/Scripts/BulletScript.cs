using UnityEngine;

using System.Collections;

using System.Collections.Generic;


public class BulletScript : MonoBehaviour
{
    public float BulletSpeed = 20f;
    public float BulletDamage = 10f;
    public Rigidbody2D BulletRb;


    public void Start()
    {
        BulletRb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        BulletRb.linearVelocity = transform.right * BulletSpeed;
    }

    
    
 
}



