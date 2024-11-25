using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    public Timer timer;
    public Text timerText;
    public int newFontSize = 60;
    public Color winColor = Color.green;
    public GameObject winCanvas;

    // Reference to the BackgroundMusic GameObject
    public GameObject backgroundMusic;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has triggered the WinFlag!");

            if (timer.isRunning)
            {
                // Stop the timer
                timer.StopTimer();
                timerText.fontSize = newFontSize;
                timerText.color = winColor;
                timer.Win();

                // Activate the win canvas
                if (winCanvas != null)
                {
                    winCanvas.SetActive(true);
                }

                // Stop the background music
                if (backgroundMusic != null)
                {
                    MusicManager musicManager = backgroundMusic.GetComponent<MusicManager>();
                    if (musicManager != null)
                    {
                        Debug.Log("Calling StopMusic on MusicManager.");
                        musicManager.StopMusic();
                    }
                    else
                    {
                        Debug.LogWarning("MusicManager component not found on BackgroundMusic.");
                    }
                }
                else
                {
                    Debug.LogWarning("BackgroundMusic GameObject is not assigned.");
                }

                // Freeze the game
                Time.timeScale = 0f;
            }
        }
    }

    public void ResetWinState()
    {
        // Resume the game
        Time.timeScale = 1f;

        if (winCanvas != null)
        {
            winCanvas.SetActive(false);
        }
    }
}
