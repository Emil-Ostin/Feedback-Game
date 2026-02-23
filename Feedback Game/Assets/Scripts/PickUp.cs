using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] PickUpEffects effect;
    [SerializeField] ParticleSystem sparkles;
    [SerializeField] Vector2 shootAngle;
    [SerializeField] Sprite[] sprite;
    [SerializeField] Color[] pickupColor;
    [SerializeField] float strengthAdd, speedAdd;


    int enumLength, pickupEffect;

    GameObject us;
    SpriteRenderer spriteRenderer;
    Color color;


    enum PickUpEffects
    {heal, strength, speed};

    void Awake()
    {
        us = GetComponent<GameObject>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enumLength = System.Enum.GetValues(typeof(PickUpEffects)).Length;
        sparkles.Play();
    }

    void Start()
    {
        PickupEffect();
    }

    void Effects()
    {
        switch (effect)
        {
            case PickUpEffects.heal:
            {
                    spriteRenderer.sprite = sprite[0];
                    spriteRenderer.color = pickupColor[0];
                    //Debug.Log("Heal");
                break;
            }

            case PickUpEffects.strength:
            {
                    spriteRenderer.sprite = sprite[1];
                    spriteRenderer.color = pickupColor[1];
                    //Debug.Log("strength");
                break;
            }

            case PickUpEffects.speed:
            {
                    spriteRenderer.sprite = sprite[2];
                    spriteRenderer.color = pickupColor[2];
                    //Debug.Log("speed");
                 break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") == true)
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

            //PickUpSpawner.DestroyPickup(gameObject.GetComponent<GameObject>());
        }
    }

    public void PickupEffect()
    {
        pickupEffect = Random.Range(0, enumLength);
        effect = (PickUpEffects)pickupEffect;
        //Debug.Log("my effekt is: "+ effect);
        Effects();
    }
}
