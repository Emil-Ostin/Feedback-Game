using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] PickUpEffects effect;
    [SerializeField] ParticleSystem sparkles;
    [SerializeField] Vector2 shootAngle;
    [SerializeField] Sprite[] sprite;
    [SerializeField] Color[] pickupColor;


    int enumLength, pickupEffect;

    SpriteRenderer spriteRenderer;
    Color color;

    enum PickUpEffects
    {heal, strength, speed};

    void Awake()
    {
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
                    Debug.Log("Heal");
                break;
            }

            case PickUpEffects.strength:
            {
                    spriteRenderer.sprite = sprite[1];
                    spriteRenderer.color = pickupColor[1];
                    Debug.Log("strength");
                break;
            }

            case PickUpEffects.speed:
            {
                    spriteRenderer.sprite = sprite[2];
                    spriteRenderer.color = pickupColor[2];
                    Debug.Log("speed");
                 break;
            }
        }
    }

    public void PickupEffect()
    {
        pickupEffect = Random.Range(0, enumLength);
        effect = (PickUpEffects)pickupEffect;
        Debug.Log("my effekt is: "+ effect);
        Effects();
    }
}
