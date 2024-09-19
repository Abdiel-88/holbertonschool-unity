using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // Required for scene management

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;  // Player speed, editable in the Inspector
    public int health = 5;       // Variable to track health
    private int originalHealth;  // To store the initial health value
    private int score = 0;       // Variable to track score
    private int originalScore;   // To store the initial score value

    private Rigidbody rb;
    public Text scoreText;       // UI Text to display the score
    public Text healthText;      // UI Text to display the health
    public Text winLoseText;     // UI Text for win/lose message
    public Image winLoseBG;      // UI background for win/lose message

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody component attached to the Player object
        rb = GetComponent<Rigidbody>();

        // Store the initial values of health and score
        originalHealth = health;
        originalScore = score;

        // Update the score and health UI at the start of the game
        SetScoreText();
        SetHealthText();

        // Ensure WinLoseBG is inactive at the start
        winLoseBG.gameObject.SetActive(false);
    }

    // FixedUpdate is called at a fixed interval, best for handling physics
    void FixedUpdate()
    {
        // Get input for movement (WASD or Arrow Keys)
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Create a movement vector based on the input
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Apply the movement to the Rigidbody to move the Player
        rb.AddForce(movement * speed);
    }

    // Called when another collider enters the trigger collider attached to this GameObject
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            // Increment the score
            score++;
            // Update the score text on the UI
            SetScoreText();
            // Disable the Coin object
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Trap"))
        {
            // Decrement the health
            health--;
            // Update the health text on the UI
            SetHealthText();
        }
        else if (other.CompareTag("Goal"))
        {
            // Display "You Win!" message on the UI
            winLoseText.text = "You Win!";
            winLoseText.color = Color.black; // Set text color to black

            // Change the background color to green
            winLoseBG.color = Color.green;

            // Activate the Win/Lose UI elements
            winLoseBG.gameObject.SetActive(true);

            // Start coroutine to reload the scene after 3 seconds
            StartCoroutine(LoadScene(3));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
           // Display "Game Over!" message and reload scene
        winLoseText.text = "Game Over!";
        winLoseText.color = Color.white;
        winLoseBG.color = Color.red;
        winLoseBG.gameObject.SetActive(true);
        StartCoroutine(LoadScene(3));
        }

        // Check if the player presses the Esc key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
        // Load the menu scene
        SceneManager.LoadScene("menu");
        }
    }

    // Method to update the score UI
    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    // Method to update the health UI
    void SetHealthText()
    {
        healthText.text = "Health: " + health.ToString();
    }

    // Coroutine to reload the scene after a delay
    IEnumerator LoadScene(float seconds)
    {
        // Wait for the specified number of seconds
        yield return new WaitForSeconds(seconds);

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Reset health and score after reloading
        health = originalHealth;
        score = originalScore;

        // Update the score and health UI after resetting
        SetScoreText();
        SetHealthText();
    }
}
