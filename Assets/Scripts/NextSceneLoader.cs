using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneLoader : MonoBehaviour
{
    
    private void OnEnable()
    {
        // When this script is enabled, load the "Level1" scene, replacing the current scene
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }
}
