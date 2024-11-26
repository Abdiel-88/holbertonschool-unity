using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseCanvas;

    // Reference to the AudioMixer
    public AudioMixer audioMixer;

    // Snapshot names (ensure these match exactly with the snapshots in the Audio Mixer)
    private const string DefaultSnapshot = "Default";
    private const string PausedSnapshot = "Paused";

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

        // Apply the Paused Snapshot
        if (audioMixer != null)
        {
            Debug.Log("Switching to Paused Snapshot...");
            AudioMixerSnapshot pausedSnapshot = audioMixer.FindSnapshot(PausedSnapshot);
            if (pausedSnapshot != null)
            {
                pausedSnapshot.TransitionTo(0.5f); // Smooth transition in 0.5 seconds
                Debug.Log("Paused Snapshot applied.");
            }
            else
            {
                Debug.LogError($"Snapshot '{PausedSnapshot}' not found in AudioMixer.");
            }
        }
        else
        {
            Debug.LogWarning("AudioMixer is not assigned.");
        }
    }

    public void Resume()
    {
        isPaused = false;
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f; // Unfreeze game time

        // Apply the Default Snapshot
        if (audioMixer != null)
        {
            Debug.Log("Switching to Default Snapshot...");
            AudioMixerSnapshot defaultSnapshot = audioMixer.FindSnapshot(DefaultSnapshot);
            if (defaultSnapshot != null)
            {
                defaultSnapshot.TransitionTo(0.5f); // Smooth transition in 0.5 seconds
                Debug.Log("Default Snapshot applied.");
            }
            else
            {
                Debug.LogError($"Snapshot '{DefaultSnapshot}' not found in AudioMixer.");
            }
        }
        else
        {
            Debug.LogWarning("AudioMixer is not assigned.");
        }
    }

    public void Restart()
    {
        isPaused = false;
        Time.timeScale = 1f;

        // Reset to Default Snapshot when restarting
        if (audioMixer != null)
        {
            Debug.Log("Resetting to Default Snapshot...");
            AudioMixerSnapshot defaultSnapshot = audioMixer.FindSnapshot(DefaultSnapshot);
            if (defaultSnapshot != null)
            {
                defaultSnapshot.TransitionTo(0.1f); // Immediate transition
                Debug.Log("Default Snapshot applied.");
            }
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        isPaused = false;
        Time.timeScale = 1f;

        // Reset to Default Snapshot when returning to MainMenu
        if (audioMixer != null)
        {
            Debug.Log("Resetting to Default Snapshot...");
            AudioMixerSnapshot defaultSnapshot = audioMixer.FindSnapshot(DefaultSnapshot);
            if (defaultSnapshot != null)
            {
                defaultSnapshot.TransitionTo(0.1f); // Immediate transition
                Debug.Log("Default Snapshot applied.");
            }
        }

        SceneManager.LoadScene("MainMenu");
    }

    public void Options()
    {
        isPaused = false;
        Time.timeScale = 1f;

        // Reset to Default Snapshot when going to Options
        if (audioMixer != null)
        {
            Debug.Log("Resetting to Default Snapshot...");
            AudioMixerSnapshot defaultSnapshot = audioMixer.FindSnapshot(DefaultSnapshot);
            if (defaultSnapshot != null)
            {
                defaultSnapshot.TransitionTo(0.1f); // Immediate transition
                Debug.Log("Default Snapshot applied.");
            }
        }

        SceneManager.LoadScene("Options");
    }
}
