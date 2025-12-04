using UnityEngine;
using UnityEngine.InputSystem;

public class TempMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    Transform playerTransform;

    void Awake()
    {
        playerTransform = GetComponent<Transform>();
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
        transform.Translate(Input.GetAxis("Horizontal") * moveSpeed, Input.GetAxis("Vertical") * moveSpeed, 0);
    }
}
