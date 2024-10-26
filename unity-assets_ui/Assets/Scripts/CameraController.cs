using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;        // Reference to the player's transform
    public Vector3 offset;          // Offset from the player
    public float rotationSpeed = 5.0f; // Speed of rotation
    public float smoothSpeed = 0.125f; // Speed of smoothing
    public bool isInverted = false;    // Invert Y-axis flag

    private float yaw;        // Horizontal rotation
    private float pitch = 0f; // Vertical rotation

    void Start()
    {
        yaw = player.eulerAngles.y; // Align yaw to player's initial Y rotation
        Vector3 initialPosition = player.position + player.TransformDirection(offset);
        transform.position = initialPosition;
        transform.LookAt(player);

        // Load the saved invert Y-axis setting
        isInverted = PlayerPrefs.GetInt("InvertY", 0) == 1;
    }

    void LateUpdate()
    {
        // Check if the game is paused or if WinCanvas is active
        if (Time.timeScale == 0f)
        {
            return; // Do not allow camera movement when paused or during the win screen
        }

        // Get mouse input for rotation
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * (isInverted ? -1 : 1);

        // Adjust yaw and pitch based on input
        yaw += mouseX;
        pitch -= mouseY;

        // Clamp pitch to prevent extreme rotation
        pitch = Mathf.Clamp(pitch, -30f, 45f);

        // Calculate the new rotation
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        // Smoothly move the camera to the new position behind the player
        Vector3 desiredPosition = player.position + rotation * offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Keep the camera looking at the player
        transform.LookAt(player);
    }

    // Method to set the Y-axis inversion from OptionsMenu
    public void SetInvertY(bool invert)
    {
        isInverted = invert;
    }
}
