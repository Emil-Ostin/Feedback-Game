using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingScript : MonoBehaviour
{
    [SerializeField] GameObject cameraShake;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;

    [SerializeField] ParticleSystem muzzleflashParticle;
    [SerializeField] GameObject weapon;
    public float gunAngle;

    Camera cam;

    ScreenShake shake;

    private void Awake()
    {
        cam = Camera.main;

        shake = cameraShake.GetComponent<ScreenShake>();
    }

    void Update()
    {
        AimAtMouse();
        FlipGun();

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Shoot();
        }
    }

    void AimAtMouse()
    {
        Vector3 mouseWorldPos =
            Camera.main.ScreenToWorldPoint(new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, 10));
        mouseWorldPos.z = 0f;
        //Debug.Log(Camera.main.ScreenToWorldPoint(new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, 10)));

        Vector2 direction = mouseWorldPos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;


        gunAngle = angle;
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        shake.startShake = true;
    }

    void SpawnMuzzleFlashParticle()
    {

    }

    void FlipGun()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, 10));

        if (mousePos.x > transform.position.x)
        {
            transform.localRotation = Quaternion.Euler(0, 0, gunAngle);
            weapon.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 180, gunAngle);
            weapon.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}

