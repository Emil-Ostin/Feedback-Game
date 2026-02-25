using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Pick Up") == true)
        {
            HealthController healthController = new HealthController();
            if (healthController.currentHealth < healthController.startHealth && effect == PickUpEffects.heal)
                healthController.currentHealth = healthController.currentHealth + 1;

            BulletScript bulletScript = new BulletScript();
            if (effect == PickUpEffects.strength)
                bulletScript.bulletDamage = bulletScript.bulletDamage + strengthAdd;

            PlayerMovement playerMovement = new PlayerMovement();
            if (effect == PickUpEffects.speed)
                playerMovement.moveSpeed = playerMovement.moveSpeed + speedAdd;
        }
    }
}
