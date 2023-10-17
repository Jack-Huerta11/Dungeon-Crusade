using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    public GameObject chestPrefab;
    public int numberOfChests = 10; // Adjust the number of chests as needed
    public Mesh terrainMesh; // Reference to your generated mesh

    private void Start()
    {
        for (int i = 0; i < numberOfChests; i++)
        {
            // Randomly generate positions on the mesh
            Vector3 randomPosition = GenerateRandomPositionOnMesh();

            // Instantiate a chest prefab at the random position
            Instantiate(chestPrefab, randomPosition, Quaternion.identity);
        }
    }

    Vector3 GenerateRandomPositionOnMesh()
    {
        // Assuming your mesh has a defined size or bounds
        Vector3 meshBounds = terrainMesh.bounds.size;

        // Randomly generate x and z coordinates within the mesh's bounds
        float randomX = Random.Range(0f, meshBounds.x);
        float randomZ = Random.Range(0f, meshBounds.z);

        // Adjust the y coordinate based on the mesh height or elevation data
        float randomY = CalculateMeshHeightAt(randomX, randomZ);

        return new Vector3(randomX, randomY, randomZ);
    }

    float CalculateMeshHeightAt(float x, float z)
    {
        // Use your map data or mesh elevation data to determine the height at a given (x, z) coordinate
        // You may need to sample the mesh's height map or use raycasting to find the mesh height.

        // Replace this with your actual code to retrieve the mesh height
        return 0f; // Temporary placeholder value
    }
}
