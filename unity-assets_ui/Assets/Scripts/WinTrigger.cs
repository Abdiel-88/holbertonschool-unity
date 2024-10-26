using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    public Timer timer;            // Reference to the Timer script
    public Text timerText;         // Reference to the TimerText UI element
    public int newFontSize = 60;   // New font size after the player wins
    public Color winColor = Color.green;  // Color change when the player wins
    public GameObject winCanvas;   // Reference to the WinCanvas prefab

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

                // Call the Win() method from the Timer script to display the final time
                timer.Win();

                // Activate the WinCanvas
                if (winCanvas != null)
                {
                    winCanvas.SetActive(true);
                }

                // Stop the player's movement and other controls
                Time.timeScale = 0f;
            }
        }
    }
}
