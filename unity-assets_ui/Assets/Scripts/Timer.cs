using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;           // Reference to TimerText UI
    public Text finalTimeText;       // Reference to FinalTime UI
    public bool isRunning = false;   // To control if the timer is running or not

    private float startTime;         // Time when the timer starts

    void Start()
    {
        // Find the FinalTime text component even if it’s inactive
        if (finalTimeText == null)
        {
            Text[] allTextComponents = FindObjectsOfType<Text>(true); // Find all Text components, including inactive ones
            foreach (Text text in allTextComponents)
            {
                if (text.name == "FinalTime")
                {
                    finalTimeText = text;
                    Debug.Log("FinalTimeText assigned programmatically.");
                    break;
                }
            }

            if (finalTimeText == null)
            {
                Debug.LogError("FinalTimeText still not found!");
            }
        }
    }

    void Update()
    {
        if (isRunning)
        {
            float elapsedTime = Time.time - startTime;
            string minutes = ((int)elapsedTime / 60).ToString();
            string seconds = (elapsedTime % 60).ToString("f2");
            timerText.text = minutes + ":" + seconds;
        }
    }

    public void StartTimer()
    {
        startTime = Time.time;
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void Win()
    {
        StopTimer();

        float elapsedTime = Time.time - startTime;
        string minutes = ((int)elapsedTime / 60).ToString();
        string seconds = (elapsedTime % 60).ToString("f2");

        if (finalTimeText != null)
        {
            finalTimeText.text = minutes + ":" + seconds;
            Debug.Log("FinalTime updated successfully.");
        }
        else
        {
            Debug.LogError("FinalTimeText is still not assigned!");
        }
    }
}
