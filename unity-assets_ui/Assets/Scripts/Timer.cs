using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;  // Reference to the TimerText UI element
    public bool isRunning = false;  // To control if the timer is running or not

    private float startTime;  // Time when the timer starts

    void Update()
    {
        if (isRunning)
        {
            // Calculate time elapsed since the start
            float elapsedTime = Time.time - startTime;

            // Format the time as minutes:seconds.milliseconds
            string minutes = ((int)elapsedTime / 60).ToString();
            string seconds = (elapsedTime % 60).ToString("f2");

            // Update the timer text
            timerText.text = minutes + ":" + seconds;
        }
    }

    public void StartTimer()
    {
        startTime = Time.time;  // Capture the start time
        isRunning = true;  // Start the timer
    }

    public void StopTimer()
    {
        isRunning = false;  // Stop the timer
    }
}
