using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    public Timer timer;
    public Text timerText;
    public int newFontSize = 60;
    public Color winColor = Color.green;
    public GameObject winCanvas;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (timer.isRunning)
            {
                timer.StopTimer();
                timerText.fontSize = newFontSize;
                timerText.color = winColor;
                timer.Win();

                if (winCanvas != null)
                {
                    winCanvas.SetActive(true);
                }

                Time.timeScale = 0f;
            }
        }
    }

    public void ResetWinState()
    {
        Time.timeScale = 1f;

        if (winCanvas != null)
        {
            winCanvas.SetActive(false);
        }
    }
}
