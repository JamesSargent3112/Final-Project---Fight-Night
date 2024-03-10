using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathMenu : MonoBehaviour
{
    public GameObject deathPanel; // Reference to the death panel UI GameObject

    void Start()
    {
        deathPanel.SetActive(false); // Ensure the death panel is not visible at the start
    }

    public void ShowDeathPanel()
    {
        deathPanel.SetActive(true); // Make the death panel visible
        Time.timeScale = 0; // Pause the game when the death panel is shown (optional)
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
        Time.timeScale = 1; // Resume game time
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Load the "MainMenu" scene
        Time.timeScale = 1; // Resume game time
    }

    public void QuitGame()
    {
        Application.Quit(); // Quit the game application
    }
}