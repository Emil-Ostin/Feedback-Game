using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] PickUpEffects effect;
    [SerializeField] ParticleSystem sparkles;
    [SerializeField] Vector2 shootAngle;
    [SerializeField] Sprite[] sprite;
    [SerializeField] Color[] pickupColor;
    [SerializeField] float strengthAdd, speedAdd;
    [SerializeField] AudioClip[] falling;

    [SerializeField] GameObject destroyParticle;


    int enumLength, pickupEffect;
    //int fallInt;

    GameObject us;
    SpriteRenderer spriteRenderer;
    //AudioSource audioSource;
    Color color;


    enum PickUpEffects
    {heal, strength, speed};

    void Awake()
    {
        us = GetComponent<GameObject>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enumLength = System.Enum.GetValues(typeof(PickUpEffects)).Length;
        //audioSource = GetComponent<AudioSource>();
        sparkles.Play();
    }

    void Start()
    {
        PickupEffect();

        //audioSource.PlayOneShot(falling[fallInt]);
    }

    private void FixedUpdate()
    {
        //fallInt = Random.Range(0, falling.Length);
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
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log(destroyParticle);
            Instantiate(destroyParticle, transform.position, Quaternion.identity);

            Debug.Log("Collideee wiithh mee");


            //HealthController healthController = gameObject.AddComponent<HealthController>();
            //if (healthController.currentHealth < healthController.startHealth && effect == PickUpEffects.heal)
            //    healthController.currentHealth = healthController.currentHealth + 1;

            //BulletScript bulletScript = gameObject.AddComponent<BulletScript>();
            //if (effect == PickUpEffects.strength)
            //    bulletScript.bulletDamage = bulletScript.bulletDamage + strengthAdd;

            //PlayerMovement playerMovement = gameObject.AddComponent<PlayerMovement>();
            //if (effect == PickUpEffects.speed)
            //    playerMovement.moveSpeed = playerMovement.moveSpeed + speedAdd;

          

            Destroy(gameObject);
            //PickUpSpawner.DestroyPickup(gameObject.GetComponent<GameObject>());
        }
    }

    private void OnDestroy()
    {
        
    }

    public void PickupEffect()
    {
        pickupEffect = Random.Range(0, enumLength);
        effect = (PickUpEffects)pickupEffect;
        //Debug.Log("my effekt is: "+ effect);
        Effects();
    }
}
