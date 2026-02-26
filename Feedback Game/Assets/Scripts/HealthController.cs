using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour
{
    [SerializeField] public int startHealth;
    public int currentHealth;
    public bool isDead = false;
    [SerializeField] bool isPlayer;

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

    private void Update()
    {
        if (isPlayer && isDead)
        {
            SceneManager.LoadScene(1);
        }
    }
}
