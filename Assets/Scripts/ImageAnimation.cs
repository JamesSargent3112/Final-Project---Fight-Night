using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))] // This class requires an Image component to be attached
public class ImageAnimation : MonoBehaviour
{
    public Sprite[] sprites; // Array of sprites to animate
    public int framesPerSprite = 6; // Number of frames to display each sprite
    public bool loop = true; // Whether the animation should loop or not
    public bool destroyOnEnd = false; // Whether to destroy the GameObject after the animation ends

    private int index = 0; // Index of the current sprite in the sprites array
    private Image image; // Reference to the Image component
    private int frame = 0; // Current frame of the animation

    void Awake()
    {
        image = GetComponent<Image>(); // Get the Image component
    }

    void FixedUpdate()
    {
        // If not looping and the animation has finished, return
        if (!loop && index == sprites.Length)
            return;

        frame++; // Increment the current frame

        // If the current frame is less than framesPerSprite, return
        if (frame < framesPerSprite)
            return;

        // Set the sprite of the Image component to the current sprite in the sprites array
        image.sprite = sprites[index];
        frame = 0; // Reset the frame counter
        index++; // Move to the next sprite in the array

        // If we've reached the end of the sprites array
        if (index >= sprites.Length)
        {
            if (loop) // If looping, reset the index to 0
                index = 0;
            if (destroyOnEnd) // If destroyOnEnd is true, destroy the GameObject
                Destroy(gameObject);
        }
    }
}