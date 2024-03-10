using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image[] hearts; // Array of heart Images, assigned in the Inspector
    public Sprite fullHeart; // Sprite for a full heart
    public Sprite threeQuarterHeart; // Sprite for a three-quarter heart
    public Sprite halfHeart; // Sprite for a half heart
    public Sprite quarterHeart; // Sprite for a quarter heart
    public Sprite emptyHeart; // Sprite for an empty heart

    public PlayerHealth playerHealth; // Reference to the PlayerHealth script

    void Update()
    {
        UpdateHearts(); // Update the heart UI every frame
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (playerHealth.currentHealth >= (i + 1) * playerHealth.maxHealth / hearts.Length)
                hearts[i].sprite = fullHeart; // Set the heart to a full heart sprite
            else if (playerHealth.currentHealth >= (i + 0.75) * playerHealth.maxHealth / hearts.Length)
                hearts[i].sprite = threeQuarterHeart; // Set the heart to a three-quarter heart sprite
            else if (playerHealth.currentHealth >= (i + 0.5) * playerHealth.maxHealth / hearts.Length)
                hearts[i].sprite = halfHeart; // Set the heart to a half heart sprite
            else if (playerHealth.currentHealth >= (i + 0.25) * playerHealth.maxHealth / hearts.Length)
                hearts[i].sprite = quarterHeart; // Set the heart to a quarter heart sprite
            else
                hearts[i].sprite = emptyHeart; // Set the heart to an empty heart sprite
        }
    }
}