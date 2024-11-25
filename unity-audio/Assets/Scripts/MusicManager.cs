using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource bgmAudio;

    void Start()
    {
        // Get the Audio Source component
        bgmAudio = GetComponent<AudioSource>();

        // Check if an AudioClip is assigned and start playing it
        if (bgmAudio != null && bgmAudio.clip != null)
        {
            Debug.Log("Playing background music: " + bgmAudio.clip.name);
            bgmAudio.Play();
        }
        else
        {
            Debug.LogWarning("No AudioClip assigned to the Audio Source.");
        }
    }

    public void StopMusic()
    {
        // Stop the music if it is playing
        if (bgmAudio != null && bgmAudio.isPlaying)
        {
            Debug.Log("Stopping background music.");
            bgmAudio.Stop();
        }
        else
        {
            Debug.LogWarning("No music to stop or AudioSource is null.");
        }
    }
}
