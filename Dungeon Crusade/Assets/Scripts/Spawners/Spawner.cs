using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] objectPrefabs; // An array of game objects to spawn
    public GameObject[] itemPrefabs;   // An array of item prefabs (the root game objects)
    public GameObject[] enemyPrefabs;  // An array of enemy prefabs

    public Mesh mesh; // Reference to the generated mesh
    public Collider meshCollider; // The collider component on the mesh

    public float objectSpawnInterval = 2.0f; // Time interval between object spawns
    public float itemSpawnInterval = 3.0f; // Time interval between item spawns

    private int objectsSpawned = 0;
    private int itemsSpawned = 0;

    private void Start()
    {
        // Start spawning objects and items at their respective intervals
        StartCoroutine(SpawnObjects());
        StartCoroutine(SpawnItems());
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            // Check if the limit for object spawns is reached
            if (objectsSpawned < 5)
            {
                // Randomly select an object prefab from the array
                int objectPrefabIndex = Random.Range(0, objectPrefabs.Length);

                // Generate a random position on the mesh
                Vector3 randomPosition = GenerateRandomPositionOnMesh();

                GameObject spawnedObject = Instantiate(objectPrefabs[objectPrefabIndex], randomPosition, Quaternion.identity);

                objectsSpawned++;
            }

            yield return new WaitForSeconds(objectSpawnInterval);
        }
    }

    IEnumerator SpawnItems()
    {
        while (true)
        {
            // Check if the limit for item spawns is reached
            if (itemsSpawned < 5)
            {
                // Randomly select an item prefab from the array
                int itemPrefabIndex = Random.Range(0, itemPrefabs.Length);

                // Generate a random position on the mesh
                Vector3 randomPosition = GenerateRandomPositionOnMesh();

                // Create an instance of the item prefab
                GameObject spawnedItemObject = Instantiate(itemPrefabs[itemPrefabIndex], randomPosition, Quaternion.identity);

                itemsSpawned++;
            }
            else
            {
                // After spawning 5 items, start spawning only enemies
                int enemyPrefabIndex = Random.Range(0, enemyPrefabs.Length);
                Vector3 randomPosition = GenerateRandomPositionOnMesh();
                GameObject spawnedEnemy = Instantiate(enemyPrefabs[enemyPrefabIndex], randomPosition, Quaternion.identity);
            }

            yield return new WaitForSeconds(itemSpawnInterval);
        }
    }

    Vector3 GenerateRandomPositionOnMesh()
    {
        // Use the collider to get the bounds of the mesh
        Bounds bounds = meshCollider.bounds;

        // Generate random x and z coordinates within the bounds of the mesh
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        // Use a raycast to find the height (y coordinate) of the mesh at the random position
        Ray ray = new Ray(new Vector3(randomX, bounds.max.y, randomZ), Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            return hit.point;
        }

        // Default to a position within the bounds if the raycast doesn't hit
        return new Vector3(randomX, bounds.min.y, randomZ);
    }
}
