using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSkipManager : MonoBehaviour
{
    void Update()
    {
        // If the 'E' key is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Skipping intro...");
            // Load the "Level1" scene, replacing the current scene
            SceneManager.LoadScene("Level1", LoadSceneMode.Single);
        }
    }
}
