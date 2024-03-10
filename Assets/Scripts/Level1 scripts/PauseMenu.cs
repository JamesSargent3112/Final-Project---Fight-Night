using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI; // Reference to the pause menu UI GameObject

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) // Check if the "M" key is pressed
        {
            if (Time.timeScale == 1) // If the game is not paused
            {
                Pause(); // Call the Pause method
            }
            else
            {
                Resume(); // Otherwise, call the Resume method
            }
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true); // Show the pause menu UI
        Time.timeScale = 0f; // Pause the game by setting the time scale to 0
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Hide the pause menu UI
        Time.timeScale = 1f; // Resume the game by setting the time scale to 1
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Ensure the time scale is reset before reloading
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f; // Ensure the time scale is reset before changing scenes
        SceneManager.LoadScene("MainMenu"); // Load the main menu scene
    }
}