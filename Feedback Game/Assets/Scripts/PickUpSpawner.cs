using UnityEngine;
public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private float angle; // force direction. the angle of the force in degrees
    [SerializeField] private float force; // force amount that will be added 
    [SerializeField] GameObject pickUpPrefab;
    [SerializeField] GameObject destroyParticle;
    [SerializeField] GameObject[] pickUpInstances;
    [SerializeField] Transform spawnCenter;
    [SerializeField]
    float
        spawnBoundary = 5f,
        spawnPosition,
        despawnTime = 2f,
        spawnTime = 1f;

    [SerializeField]
    int
       pickuplimit = 10;

    private void Awake()
    {
        InvokeRepeating("SpawnPickup", spawnTime, spawnTime);
    }

    void FixedUpdate()
    {
        pickUpInstances = GameObject.FindGameObjectsWithTag("Pick Up");
    }

    void SpawnPickup()
    {
        if (pickUpInstances.Length +1 <= pickuplimit)
        {
            float spawnX = spawnCenter.position.x;
            spawnPosition = UnityEngine.Random.Range(spawnX, spawnX + spawnBoundary);

            GameObject thisInstanceOfPrefab = Instantiate(pickUpPrefab, new Vector2(spawnPosition, spawnCenter.position.y), Quaternion.identity);
            Rigidbody2D pickupBodyInstance = thisInstanceOfPrefab.GetComponent<Rigidbody2D>();

            pickupBodyInstance.AddForce
                (new Vector2
                (Mathf.Cos(angle * Mathf.Deg2Rad) * force, 
                Mathf.Sin(angle * Mathf.Deg2Rad) * force), 
                ForceMode2D.Impulse);
            
            //Debug.Log("intanciated " + thisInstanceOfPrefab.name);
            
            Invoke("DespawnPickup", despawnTime);
        }
        else { return; }
    }

    void DespawnPickup()
    { 
            GameObject oldestPickup = pickUpInstances[0];

            Instantiate(destroyParticle, oldestPickup.transform.position, Quaternion.identity);

            Destroy(oldestPickup);
    }

    public void DestroyPickup(GameObject pickup)
    {
        Instantiate(destroyParticle, pickup.transform.position, Quaternion.identity);

        Destroy(pickup);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(spawnCenter.position.x, spawnCenter.position.y),
            new Vector3(spawnCenter.position.x + spawnBoundary, spawnCenter.position.y));
    }
}
