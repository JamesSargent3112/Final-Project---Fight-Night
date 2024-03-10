using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthValue = 25; // The amount of health this pickup will restore
    public AudioSource pickupSound; // Reference to the AudioSource component

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null && !playerHealth.IsFullHealth()) // Check if the collider has a PlayerHealth component and if the player's health is not full
        {
            playerHealth.Heal(healthValue); // Heal the player

            pickupSound.Play(); // Play the pickup sound

            GetComponent<SpriteRenderer>().enabled = false; // Disable the sprite renderer
            GetComponent<Collider2D>().enabled = false; // Disable the collider

            Destroy(gameObject, pickupSound.clip.length); // Destroy the pickup after the sound has finished playing
        }
    }
}