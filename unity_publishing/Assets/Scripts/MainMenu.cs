using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Material trapMat;        // Trap material
    public Material goalMat;        // Goal material
    public Toggle colorblindMode;   // Colorblind mode toggle

    public void PlayMaze()
    {
        // Check if Colorblind Mode is enabled
        if (colorblindMode.isOn)
        {
            // Change the trap and goal colors for colorblind mode
            trapMat.color = new Color32(255, 112, 0, 1); // Orange for traps
            goalMat.color = Color.blue;                  // Blue for goal
        }
        else
        {
            // Set the default trap and goal colors
            trapMat.color = Color.red;   // Default red for traps
            goalMat.color = Color.green; // Default green for goal
        }

        // Load the maze scene
        SceneManager.LoadScene("maze");
    }

    public void QuitMaze()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}


