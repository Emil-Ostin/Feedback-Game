using UnityEngine;

public class HealthController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] int startHealth;

    [Header("Live Stats")]
    public int currentHealth;
    public bool isDead = false;
    public bool takenDamage;

    int takenDamageAmount;

    private void Awake()
    {
        currentHealth = startHealth;
    }

    private void FixedUpdate()
    {
        TakenDamage();

        takenDamageAmount = 0;
    }

    public void Health(int damageAmount)
    {
        currentHealth -= damageAmount;

        takenDamageAmount = damageAmount;

        if (currentHealth <= 0) { isDead = true; }
    }

    public void TakenDamage()
    {
        if (takenDamageAmount > 0) { takenDamage = true; }
        else { takenDamage = false; }
    }
}
