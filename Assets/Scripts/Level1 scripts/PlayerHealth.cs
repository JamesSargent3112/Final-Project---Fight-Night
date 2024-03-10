using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health of the player
    public int currentHealth; // Current health of the player
    public Animator animator; // Reference to the Animator component for animation events
    public AudioSource playerDeath; // Reference to the AudioSource component for playing death sound

    public void Start()
    {
        currentHealth = maxHealth; // Initialize the current health to the maximum health
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduce the current health by the damage amount
        Debug.Log($"Player took {damage} damage, current health: {currentHealth}"); // Log the damage taken and current health
        animator.SetTrigger("Hurt"); // Trigger the "Hurt" animation

        if (currentHealth <= 0) // If the current health is 0 or less
        {
            playerDeath.Play(); // Play the death sound
            Die(); // Call the Die method
            StartCoroutine(WaitAndShowDeathPanel(2f)); // Start the coroutine to show the death panel after 2 seconds
        }
    }

    IEnumerator WaitAndShowDeathPanel(float waitTime)
    {
        yield return new WaitForSeconds(waitTime); // Wait for the specified waitTime
        FindObjectOfType<PlayerDeathMenu>().ShowDeathPanel(); // Show the death panel
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount; // Increase the current health by the heal amount
        currentHealth = Mathf.Min(currentHealth, maxHealth); // Ensure health does not exceed maxHealth
        Debug.Log($"Player healed {healAmount}, current health: {currentHealth}"); // Log the healing amount and current health
    }

    public bool IsFullHealth()
    {
        return currentHealth >= maxHealth; // Return true if the current health is greater than or equal to the maximum health
    }

    void Die()
    {
        Debug.Log("Player has died!"); // Log a message indicating the player's death
        animator.SetTrigger("Die"); // Trigger the "Die" animation
    }

    public bool IsDead()
    {
        return currentHealth <= 0; // Return true if the current health is 0 or less
    }
}