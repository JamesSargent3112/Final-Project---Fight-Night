using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance; // Singleton instance

    public int enemiesLeftInArea; // Number of enemies remaining in the current area
    public GameObject[] sectionBoundaries; // Forward boundaries for each section
    public GameObject[] backBoundaries; // Back boundaries for each section
    private int currentSectionIndex = 0; // Index of the current section
    public bool canAdvance = false; // Flag to control the player's ability to advance to the next section
    public GameObject[] transitionTriggers; // Array of triggers for section transitions
    private List<GameObject> currentEnemies = new List<GameObject>(); // List of current enemies
    public GameObject[] spawnerParentsInSection; // Array of parent GameObjects for spawners in each section
    public BlinkingArrow blinkingArrow; // Reference to the BlinkingArrow script
    private int totalSpawnersActive; // Total number of active spawners in the current section
    private int spawnersCompleted; // Number of completed spawners in the current section

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        currentSectionIndex = 0; // Set the start of the game to index 0
    }

    void Start()
    {
        InitializeEnemyCount();

        foreach (var trigger in transitionTriggers) // Initially disable all transition triggers
        {
            if (trigger != null) trigger.SetActive(false);
        }

        ActivateSectionSpawners(); // Activate spawners for the starting section
    }

    public void RegisterSpawner()
    {
        totalSpawnersActive++;
    }

    public void SpawnerCompleted()
    {
        spawnersCompleted++;
        CheckForSectionCompletion();
    }

    private void CheckForSectionCompletion()
    {
        Debug.Log($"Checking for completion: Spawners Completed = {spawnersCompleted}, Total Spawners Active = {totalSpawnersActive}, Enemies Left = {enemiesLeftInArea}");
        if (spawnersCompleted == totalSpawnersActive && enemiesLeftInArea == 0)
        {
            Debug.Log("Conditions met for section completion.");
            EnableSectionAdvance();
        }
    }

    private void EnableSectionAdvance()
    {
        canAdvance = true;
        blinkingArrow.SetVisibility(true); // Make the arrow visible
        blinkingArrow.StartBlinking(); // Arrow starts blinking
        if (currentSectionIndex < transitionTriggers.Length) // Enable the trigger for the next section, if applicable
        {
            transitionTriggers[currentSectionIndex].SetActive(true);
        }
    }

    public void InitializeEnemyCount()
    {
        enemiesLeftInArea = GameObject.FindGameObjectsWithTag("Enemy").Length; // Find all game objects with the "Enemy" tag and set the enemiesLeftInArea to the count
    }

    public void RegisterEnemy(GameObject enemy)
    {
        if (!currentEnemies.Contains(enemy)) // Check if the enemy is not already in the list
        {
            currentEnemies.Add(enemy); // Add the enemy to the list
            enemiesLeftInArea++; // Increment the count of enemies left in the area
        }
    }

    public void EnemyDefeated()
    {
        enemiesLeftInArea--; // Decrement the count of enemies left in the area
        CheckForSectionCompletion(); // Check if the conditions for section completion are met
    }

    public void AdvanceToNextSection()
    {
        if (canAdvance) // Check if the player is allowed to advance to the next section
        {
            blinkingArrow.StopBlinking(); // Stop the blinking arrow
            blinkingArrow.SetVisibility(false); // Hide the blinking arrow

            // Deactivate the current section's forward boundary
            if (currentSectionIndex < sectionBoundaries.Length)
            {
                sectionBoundaries[currentSectionIndex].SetActive(false);
            }

            currentSectionIndex++; // Increment the section index to move to the next section

            // Activate the next section's forward boundary, if it exists
            if (currentSectionIndex < sectionBoundaries.Length)
            {
                sectionBoundaries[currentSectionIndex].SetActive(true);
            }

            // Deactivate the back boundary of the previous section to prevent going back
            if (currentSectionIndex - 1 >= 0 && currentSectionIndex - 1 < backBoundaries.Length)
            {
                backBoundaries[currentSectionIndex - 1].SetActive(false);
            }

            // Activate the back boundary of the current section
            if (currentSectionIndex < backBoundaries.Length)
            {
                backBoundaries[currentSectionIndex].SetActive(true);
            }

            canAdvance = false; // Reset the flag to prevent multiple transitions

            // Disable the transition trigger to prevent re-activation
            if (currentSectionIndex - 1 < transitionTriggers.Length)
            {
                transitionTriggers[currentSectionIndex - 1].SetActive(false);
            }

            OnPlayerEnteredSection(); // Call the method to handle the player entering a new section

            // Inform the CameraFollow script to update its boundaries for the new section
            CameraFollow cameraFollow = FindObjectOfType<CameraFollow>();
            if (cameraFollow != null)
            {
                cameraFollow.AdvanceToNextSection(currentSectionIndex);
            }
        }
    }

    public void OnPlayerEnteredSection()
    {
        // Functionality for managing boundaries
        if (canAdvance)
        {
            if (currentSectionIndex - 1 >= 0 && currentSectionIndex - 1 < backBoundaries.Length)
            {
                backBoundaries[currentSectionIndex - 1].SetActive(false);
            }

            if (currentSectionIndex < backBoundaries.Length)
            {
                backBoundaries[currentSectionIndex].SetActive(true);
            }

            canAdvance = false;
        }

        ActivateSectionSpawners(); // Activate the enemy spawners for the new section
    }

    void ActivateSectionSpawners()
    {
        spawnersCompleted = 0; // Reset the completed spawner count
        totalSpawnersActive = 0; // Reset the total active spawners count

        // Check if there are spawner parents assigned for the current section
        if (spawnerParentsInSection != null && spawnerParentsInSection.Length > currentSectionIndex)
        {
            GameObject parent = spawnerParentsInSection[currentSectionIndex]; // Get the parent GameObject for the spawners in the current section
            if (parent != null)
            {
                // Activate each spawner found as a child of the parent GameObject
                foreach (Transform child in parent.transform)
                {
                    EnemySpawner spawner = child.GetComponent<EnemySpawner>();
                    if (spawner != null)
                    {
                        totalSpawnersActive++; // Increment the total active spawners count
                        spawner.ActivateSpawner(); // Activate the spawner
                    }
                }
            }
        }

        if (totalSpawnersActive == 0)
        {
            Debug.Log("No spawners active in this section."); // Log a message if no spawners are active
            EnableSectionAdvance(); // Allow immediate advancement if no enemies will spawn (used for testing due to quickness)
        }
    }
}