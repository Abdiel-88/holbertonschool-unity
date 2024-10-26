using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    // Method to go back to the Main Menu
    public void MainMenu()
    {
        Debug.Log("Returning to Main Menu...");
        SceneManager.LoadScene("MainMenu");
    }

    // Method to load the next level
    public void Next()
    {
        Debug.Log("Loading Next Level...");
        
        // Get the current active scene's build index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Check if there's a next level to load
        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            // Load the next scene
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            // If this is the last level, go to the Main Menu
            SceneManager.LoadScene("MainMenu");
        }
    }
}
