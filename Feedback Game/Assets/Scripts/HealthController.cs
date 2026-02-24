using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] public int startHealth;
    public int currentHealth;
    public bool isDead = false;

    Animator animator;

    private void Awake()
    {
        currentHealth = startHealth;

        animator = GetComponent<Animator>();
    }

    public void Health(int damageAmount)
    {
        currentHealth -= damageAmount;

        animator.Play("EnemyAnim");

        if (currentHealth <= 0) { isDead = true; }
    }
}
