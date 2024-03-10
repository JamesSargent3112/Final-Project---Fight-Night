using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50; // Maximum health of the enemy
    private int currentHealth; // Current health of the enemy
    public Animator animator; // Reference to the Animator component for animation events
    public bool isDead = false; // Flag to track if the enemy is dead

    [SerializeField] private GameObject healthPickupPrefab; // Reference to the health pickup prefab, assigned in the Inspector
    [SerializeField] private float dropChance = 0.25f; // Chance for the enemy to drop a health pickup when killed

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // Initialize the current health to the maximum health
    }

    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        if (isDead) return; // Do not take damage if the enemy is already dead

        currentHealth -= damage; // Reduce the current health by the damage amount
        animator.SetTrigger("Hurt"); // Trigger the "Hurt" animation

        if (currentHealth <= 0)
        {
            Die(); // Call the Die method if the current health is 0 or less
        }
    }

    void Die()
    {
        if (isDead) return; // Prevent the Die method from being called multiple times

        Debug.Log(gameObject.name + " Died!"); // Log a message to the console indicating the enemy's death
        animator.SetTrigger("Die"); // Trigger the "Die" animation
        GetComponent<AIFollowPlayer>().enabled = false; // Disable the AIFollowPlayer component to stop enemy movement
        GetComponent<Collider2D>().isTrigger = true; // Make the enemy's collider a trigger to avoid further interaction after death
        isDead = true; // Set the isDead flag to true

        if (gameObject.CompareTag("Boss")) // Check if the enemy is a boss
        {
            // If this is the boss, do boss-specific logic
            FindObjectOfType<NextLevelTrigger>().ShowEndLevelMenu(); // Show the end level menu
            LevelManager.instance.EnemyDefeated(); // Notify the LevelManager that an enemy has been defeated
        }
        else
        {
            // If it's not the boss, do regular enemy logic
            LevelManager.instance.EnemyDefeated(); // Notify the LevelManager that an enemy has been defeated
        }

        TryDropHealthPickup(); // Attempt to drop a health pickup

        Destroy(gameObject, 2f); // Destroy the enemy game object after 2 seconds so the animation plays out
    }

    private void TryDropHealthPickup()
    {
        if (Random.value < dropChance) // Check if a random value is less than the drop chance
        {
            Instantiate(healthPickupPrefab, transform.position, Quaternion.identity); // Instantiate a health pickup prefab at the enemy's position
        }
    }
}