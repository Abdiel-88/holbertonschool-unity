using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    public Timer timer;           // Reference to the Timer script
    public Text timerText;        // Reference to the TimerText UI element
    public int newFontSize = 60;  // New font size after the player wins
    public Color winColor = Color.green;  // Color change when the player wins

    private void OnTriggerEnter(Collider other)
    {
        // Check if the Player entered the WinFlag collider
        if (other.CompareTag("Player"))
        {
            // Stop the timer if it's running
            if (timer.isRunning)
            {
                timer.StopTimer();  // Stop the timer

                // Increase the font size
                timerText.fontSize = newFontSize;

                // Change the text color to green
                timerText.color = winColor;
            }
        }
    }
}
