using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        // Check if the Player exited the trigger
        if (other.CompareTag("Player"))
        {
            // Find the Timer script attached to the Player
            Timer timer = other.GetComponent<Timer>();

            // Enable the Timer and start counting
            if (timer != null && !timer.enabled)
            {
                timer.enabled = true;  // Enable the Timer script
                timer.StartTimer();    // Start the timer
            }
        }
    }
}
