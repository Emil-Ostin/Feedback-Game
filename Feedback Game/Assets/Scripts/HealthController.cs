using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] int healthAmount;

    public void Health(int damageAmount)
    {
        if (healthAmount <= 0)
        {
            Debug.Log("Death");
        }

        healthAmount -= damageAmount;
    }
}
