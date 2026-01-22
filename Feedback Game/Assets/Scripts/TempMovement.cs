using UnityEngine;
using UnityEngine.InputSystem;

public class TempMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    Transform playerTransform;

    HealthController health;

    void Awake()
    {
        playerTransform = GetComponent<Transform>();
        health = GetComponent<HealthController>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        HandleMove();
    }

    void HandleMove()
    {
        if (health.isDead == true) { return; }

        transform.Translate(Input.GetAxis("Horizontal") * moveSpeed, Input.GetAxis("Vertical") * moveSpeed, 0);
    }
}
