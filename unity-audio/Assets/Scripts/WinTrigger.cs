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

    // Reference to the VictoryMusic GameObject
    public GameObject victoryMusic;

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
                        Debug.Log("Stopping background music.");
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

                // Play the victory music
                if (victoryMusic != null)
                {
                    AudioSource victoryAudio = victoryMusic.GetComponent<AudioSource>();
                    if (victoryAudio != null)
                    {
                        Debug.Log("Playing victory fanfare.");
                        victoryAudio.Play();
                    }
                    else
                    {
                        Debug.LogWarning("AudioSource component not found on VictoryMusic.");
                    }
                }
                else
                {
                    Debug.LogWarning("VictoryMusic GameObject is not assigned.");
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
