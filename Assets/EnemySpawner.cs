using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject enemyPrefab; // Reference to the enemy prefab
    public Transform spawnPoint;  // Optional: Specify a spawn point (default is the spawner's position)
    public float spawnInterval = 2.0f; // Time between spawns in seconds
    public int maxEnemiesPerWave = 5; // Number of enemies to spawn in each wave
    public float waveInterval = 5.0f; // Time between waves

    private List<GameObject> activeEnemies = new List<GameObject>(); // List of active enemies
    private bool isSpawning = false; // Whether a wave is currently being spawned

    void Start()
    {
        // Start the first wave
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        isSpawning = true;

        for (int i = 0; i < maxEnemiesPerWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }

        isSpawning = false;

        // Wait for all enemies in the current wave to be destroyed
        yield return StartCoroutine(WaitForEnemiesToDie());

        // Wait before starting the next wave
        yield return new WaitForSeconds(waveInterval);

        // Start the next wave
        StartCoroutine(SpawnWave());
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.LogWarning("Enemy Prefab is not assigned!");
            return;
        }

        // Determine the spawn position (use the spawner's position if no spawn point is specified)
        Vector3 spawnPosition = spawnPoint != null ? spawnPoint.position : transform.position;

        // Instantiate the enemy prefab
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Add the new enemy to the active enemies list
        activeEnemies.Add(enemy);
    }

    private IEnumerator WaitForEnemiesToDie()
    {
        while (activeEnemies.Count > 0)
        {
            // Clean up any null entries (destroyed enemies) in the list
            activeEnemies.RemoveAll(enemy => enemy == null);
            yield return null; // Wait until the next frame to recheck
        }
    }
}
