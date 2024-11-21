using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseCanvas;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        isPaused = true;
        pauseCanvas.SetActive(true);
        Time.timeScale = 0f; // Freeze game time
    }

    public void Resume()
    {
        isPaused = false;
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f; // Unfreeze game time
    }

    public void Restart()
    {
        isPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        isPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Options()
    {
        isPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Options");
    }
}
