using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTrigger : MonoBehaviour
{
    public GameObject endLevelMenu; // Reference to the end level menu GameObject, assigned in the Inspector

    void Start()
    {
        endLevelMenu.SetActive(false); // Ensure the menu is not visible at the start
    }

    public void ShowEndLevelMenu()
    {
        endLevelMenu.SetActive(true); // Show the end level menu
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Ensure the time scale is reset before reloading
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Load the "MainMenu" scene
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit(); // Quit the game application
    }
}