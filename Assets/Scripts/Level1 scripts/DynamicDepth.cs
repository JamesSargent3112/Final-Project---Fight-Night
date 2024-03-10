using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DynamicDepth : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
    }

    void Update()
    {
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * -100); // Set the sorting order based on the Y position of the object
        // Multiply by -100 to ensure integer values are distinct enough for sorting
    }
}