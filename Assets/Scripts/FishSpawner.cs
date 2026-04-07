using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject fishPrefab;    
    public Transform playerCamera;  
    public int numberOfFish = 5;    
    public float spawnDistance = 5.0f; 
    public float heightRange = 2.0f;   
    void Start()
    {
        SpawnFish();
    }

    void SpawnFish()
    {
        for (int i = 0; i < numberOfFish; i++)
        {
            
            float angle = Random.Range(0f, Mathf.PI * 2);
            float x = Mathf.Cos(angle) * spawnDistance;
            float z = Mathf.Sin(angle) * spawnDistance;
            float y = Random.Range(-0, heightRange);

            Vector3 spawnPos = playerCamera.position + new Vector3(x, y, z);

            
            GameObject newFish = Instantiate(fishPrefab, spawnPos, Quaternion.identity);

            PoissonMovment orbit = newFish.GetComponent<PoissonMovment>();
            if (orbit != null)
            {
                orbit.target = playerCamera;
                orbit.speed = Random.Range(10f, 30f);
            }
        }
    }
}
