using UnityEngine;

public class PlayerFootstepController : MonoBehaviour
{
    public AudioSource grassAudioSource;
    public AudioSource stoneAudioSource;

    private string currentSurface = "";

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Grass"))
        {
            currentSurface = "Grass";
        }
        else if (collision.gameObject.CompareTag("Stone"))
        {
            currentSurface = "Stone";
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Grass") || collision.gameObject.CompareTag("Stone"))
        {
            currentSurface = "";
        }
    }

    public void PlayFootstep()
    {
        if (currentSurface == "Grass")
        {
            if (!grassAudioSource.isPlaying) grassAudioSource.Play();
        }
        else if (currentSurface == "Stone")
        {
            if (!stoneAudioSource.isPlaying) stoneAudioSource.Play();
        }
    }

    public void StopFootstep()
    {
        grassAudioSource.Stop();
        stoneAudioSource.Stop();
    }
}
