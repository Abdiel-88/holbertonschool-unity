using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public Toggle InvertYToggle;
    public Slider BGMSlider; // Reference to the BGM slider
    public Slider SFXSlider; // Reference to the SFX slider
    public AudioMixer audioMixer; // Reference to the MasterMixer

    private CameraController cameraController;
    private const string BGMVolumeKey = "BGMVolume"; // Key for saving BGM volume in PlayerPrefs
    private const string SFXVolumeKey = "SFXVolume"; // Key for saving SFX volume in PlayerPrefs

    void Start()
    {
        // Load the saved state of InvertY
        InvertYToggle.isOn = PlayerPrefs.GetInt("InvertY", 0) == 1;

        // Load the saved BGM and SFX volume levels or set defaults
        float savedBGMVolume = PlayerPrefs.GetFloat(BGMVolumeKey, 0.75f);
        float savedSFXVolume = PlayerPrefs.GetFloat(SFXVolumeKey, 0.75f);
        BGMSlider.value = savedBGMVolume;
        SFXSlider.value = savedSFXVolume;

        // Apply the saved volumes to the Audio Mixer
        SetBGMVolume(savedBGMVolume);
        SetSFXVolume(savedSFXVolume);

        // Add listeners for slider value changes
        BGMSlider.onValueChanged.AddListener(SetBGMVolume);
        SFXSlider.onValueChanged.AddListener(SetSFXVolume);

        // Find the CameraController in the scene
        cameraController = FindObjectOfType<CameraController>();

        // Set the initial state of camera inversion
        if (cameraController != null)
        {
            cameraController.SetInvertY(InvertYToggle.isOn);
        }
    }

    public void Apply()
    {
        // Save the state of InvertY
        PlayerPrefs.SetInt("InvertY", InvertYToggle.isOn ? 1 : 0);

        // Save the volume levels
        PlayerPrefs.SetFloat(BGMVolumeKey, BGMSlider.value);
        PlayerPrefs.SetFloat(SFXVolumeKey, SFXSlider.value);

        PlayerPrefs.Save();

        // Update the CameraController inversion
        if (cameraController != null)
        {
            cameraController.SetInvertY(InvertYToggle.isOn);
        }

        // Return to the previous scene
        SceneManager.LoadScene(PlayerPrefs.GetString("PreviousScene", "MainMenu"));
    }

    public void Back()
    {
        // Just return to the previous scene without saving
        SceneManager.LoadScene(PlayerPrefs.GetString("PreviousScene", "MainMenu"));
    }

    public void SetBGMVolume(float sliderValue)
    {
        // Convert the slider value (0 to 1) to dB (-80 to 0)
        float dB = Mathf.Log10(Mathf.Clamp(sliderValue, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat("BGMVolume", dB);
    }

    public void SetSFXVolume(float sliderValue)
    {
        // Convert the slider value (0 to 1) to dB (-80 to 0)
        float dB = Mathf.Log10(Mathf.Clamp(sliderValue, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat("SFXVolume", dB);
    }
}
