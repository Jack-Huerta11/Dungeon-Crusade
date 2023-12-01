using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRadius = 10f;
    public float spawnInterval = 2f;

    private Transform player;

    void Start()
    {
        // Assuming your player has a "Player" tag, you can find it this way
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Start spawning enemies at intervals
        InvokeRepeating("SpawnEnemy", 5f, spawnInterval);
    }

    void SpawnEnemy()
    {
        // Calculate a random position within the spawn radius around the player
        Vector2 randomSpawnPoint = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 spawnPosition = new Vector3(randomSpawnPoint.x, 0f, randomSpawnPoint.y) + player.position;

        // Instantiate the enemy at the calculated position
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
