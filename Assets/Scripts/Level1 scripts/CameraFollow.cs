using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CameraSectionBoundaries
{
    public Vector2 minCameraPos; // Minimum camera position for this section
    public Vector2 maxCameraPos; // Maximum camera position for this section
}

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float smoothSpeed = 0.125f; // Speed at which the camera smoothly follows the player
    public List<CameraSectionBoundaries> sectionBoundaries; // List of camera section boundaries
    private CameraSectionBoundaries currentBoundaries; // Current camera section boundaries
    private Vector3 velocity = Vector3.zero; // Velocity used for smooth damping

    private void Start()
    {
        if (sectionBoundaries.Count > 0)
        {
            currentBoundaries = sectionBoundaries[0]; // Set the initial camera section boundaries
        }
    }

    private void FixedUpdate()
    {
        if (player == null || currentBoundaries == null) return; // Return if the player or current boundaries are null

        float posX = Mathf.Clamp(player.position.x, currentBoundaries.minCameraPos.x, currentBoundaries.maxCameraPos.x); // Clamp the player's X position within the current boundaries
        float posY = Mathf.Clamp(player.position.y, currentBoundaries.minCameraPos.y, currentBoundaries.maxCameraPos.y); // Clamp the player's Y position within the current boundaries

        Vector3 targetPosition = new Vector3(posX, posY, transform.position.z); // Calculate the target position for the camera
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothSpeed); // Smoothly move the camera to the target position
    }

    public void AdvanceToNextSection(int sectionIndex)
    {
        if (sectionIndex >= 0 && sectionIndex < sectionBoundaries.Count) // Check if the provided section index is valid
        {
            currentBoundaries = sectionBoundaries[sectionIndex]; // Update the current camera section boundaries
        }
        else
        {
            Debug.LogWarning($"CameraFollow::AdvanceToNextSection - Invalid sectionIndex provided: {sectionIndex}"); // Log a warning if the section index is invalid
        }
    }
}