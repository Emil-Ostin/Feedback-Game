using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    float startPos;
    float length;
    [SerializeField] GameObject mainCamera;
    [SerializeField] float parallaxSpeed;


    void Awake()
    {
        startPos = transform.position.x;

        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float distance = mainCamera.transform.position.x * parallaxSpeed;
        float movement = mainCamera.transform.position.x * (1 - parallaxSpeed);

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if (movement > startPos + length)
        {
            startPos += length;
        }
        else if (movement < startPos - length)
        {
            startPos -= length;
        }
    }
}
