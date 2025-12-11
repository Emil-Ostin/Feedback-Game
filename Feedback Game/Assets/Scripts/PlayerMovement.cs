using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Camera cam;

    [SerializeField] float moveSpeed = 10f;

    InputAction moveAction;

    Vector3 moveVector;
    void Start()
    {
        moveAction = InputSystem.actions.FindAction ("Move");

        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

        // Vector3 mousePos = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
        // float angleRad = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x);
        //float angleDeg = (180 / Mathf.PI) * angleRad - 90; // Offset this by 90 Degrees

        //transform.rotation = Quaternion.Euler(0f, 0f, angleDeg);
        //Debug.DrawLine(transform.position, mousePos, Color.white, Time.deltaTime);

        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (mousePos.x > transform.position.x)
        {
            // Face right
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            // Face left
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }


    void MovePlayer()
    {
        moveVector = moveAction.ReadValue<Vector2>();
        transform.position += moveVector * moveSpeed * Time.deltaTime;

      
     
    }
}
