using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingArrow : MonoBehaviour
{
    public Image arrowImage; // Reference to the Image component representing the arrow
    public Image textImage; // Reference to the Image component representing the text
    public float blinkInterval = 0.5f; // Interval at which the arrow and text should blink (in seconds)
    private bool isBlinking = false; // Flag to track if the blinking coroutine is currently running

    public void SetVisibility(bool isVisible)
    {
        arrowImage.gameObject.SetActive(isVisible); // Sets the active state of the arrowImage GameObject
        textImage.gameObject.SetActive(isVisible); // Sets the active state of the textImage GameObject
    }

    void Start()
    {
        SetVisibility(false); // Make the arrow and text invisible at the start
    }

    public void StartBlinking()
    {
        if (!isBlinking) // Check if the blinking coroutine is not currently running
        {
            StartCoroutine(Blink()); // Start the Blink coroutine
            isBlinking = true; // Set the isBlinking flag to true
        }
    }

    public void StopBlinking()
    {
        StopAllCoroutines(); // Stop all coroutines, including the Blink coroutine
        arrowImage.enabled = false; // Ensure the arrow image is disabled when not blinking
        textImage.enabled = false; // Ensure the text image is disabled when not blinking
        isBlinking = false; // Set the isBlinking flag to false
    }

    private IEnumerator Blink()
    {
        while (true) // Run the coroutine indefinitely until stopped
        {
            arrowImage.enabled = !arrowImage.enabled; // Toggle the visibility of the arrow image
            textImage.enabled = !textImage.enabled; // Toggle the visibility of the text image
            yield return new WaitForSeconds(blinkInterval); // Wait for the specified blink interval
        }
    }
}