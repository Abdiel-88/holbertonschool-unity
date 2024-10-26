using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public PlayerController playerController;
    public WinTrigger winTrigger;

    public void MainMenu()
    {
        Debug.Log("Returning to Main Menu...");
        ResetWinState();
        SceneManager.LoadScene("MainMenu");
    }

    public void Next()
    {
        Debug.Log("Loading Next Level...");
        ResetWinState();

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void ResetWinState()
    {
        if (winTrigger != null)
        {
            winTrigger.ResetWinState();
        }

        if (playerController != null)
        {
            playerController.ResetPlayer();
        }
    }
}
