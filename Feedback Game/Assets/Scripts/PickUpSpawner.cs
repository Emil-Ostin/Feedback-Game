using System.Collections;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
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
            spawnPosition = Random.Range(spawnX, spawnX + spawnBoundary);

            GameObject thisInstanceOfPrefab = Instantiate(pickUpPrefab, new Vector2(spawnPosition, spawnCenter.position.y), Quaternion.identity);
            Debug.Log("intanciated " + thisInstanceOfPrefab.name);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(spawnCenter.position.x, spawnCenter.position.y),
            new Vector3(spawnCenter.position.x + spawnBoundary, spawnCenter.position.y));
    }
}
