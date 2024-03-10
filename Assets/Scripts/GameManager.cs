using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton instance of the GameManager

    private void Awake()
    {
        // Implement the singleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        Debug.Log("Loading intro scene...");
        // Load the "IntroCinematic" scene
        SceneManager.LoadScene("IntroCinematic");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        // Quit the application
        Application.Quit();
    }
}