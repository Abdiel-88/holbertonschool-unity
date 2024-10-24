using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public Toggle InvertYToggle;

    void Start()
    {
        // Load the saved state of InvertY
        InvertYToggle.isOn = PlayerPrefs.GetInt("InvertY", 0) == 1;
    }

    public void Apply()
    {
        // Save the state based on the toggle
        PlayerPrefs.SetInt("InvertY", InvertYToggle.isOn ? 1 : 0);
        PlayerPrefs.Save();

        // Return to the previous scene
        SceneManager.LoadScene(PlayerPrefs.GetString("PreviousScene", "MainMenu"));
    }

    public void Back()
    {
        // Just return to the previous scene without saving
        SceneManager.LoadScene(PlayerPrefs.GetString("PreviousScene", "MainMenu"));
    }
}
