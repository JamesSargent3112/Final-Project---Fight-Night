using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab for the enemy to be spawned
    public float minSpawnDelay = 2f; // Minimum delay between enemy spawns
    public float maxSpawnDelay = 5f; // Maximum delay between enemy spawns
    public int maxOnScreen = 5; // Maximum number of enemies allowed on screen at a time
    public int maxTotalSpawns = 20; // Maximum total number of enemies to spawn

    private int spawnedCount = 0; // Number of enemies spawned so far
    private bool isActive = false; // Flag to track if the spawner is active

    public void ActivateSpawner()
    {
        if (!isActive)
        {
            isActive = true;
            StartCoroutine(SpawnEnemies()); // Start the enemy spawning coroutine
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (spawnedCount < maxTotalSpawns && isActive)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay)); // Wait for a random delay

            if (LevelManager.instance.enemiesLeftInArea < maxOnScreen) // Check if the maximum number of enemies on screen has not been reached
            {
                SpawnEnemy();
                if (spawnedCount >= maxTotalSpawns)
                {
                    DeactivateSpawner(); // Deactivate the spawner if the maximum total spawns have been reached
                }
            }
        }
    }

    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity); // Instantiate the enemy prefab
        AIFollowPlayer aiFollowPlayer = enemy.GetComponent<AIFollowPlayer>();
        if (aiFollowPlayer != null)
        {
            aiFollowPlayer.player = FindObjectOfType<PlayerMovement>().transform; // Set the player's transform for the enemy to follow
        }
        LevelManager.instance.RegisterEnemy(enemy); // Register the spawned enemy with the LevelManager
        spawnedCount++;
    }

    public void DeactivateSpawner()
    {
        isActive = false; // Stop spawning enemies
        LevelManager.instance.SpawnerCompleted(); // Notify the LevelManager that this spawner is done
    }
}