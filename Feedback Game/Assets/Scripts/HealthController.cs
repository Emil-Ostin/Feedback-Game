using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] public int startHealth;
    public int currentHealth;
    public bool isDead = false;

    private void Awake()
    {
        currentHealth = startHealth;
    }

    public void Health(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0) { isDead = true; }
    }
}
