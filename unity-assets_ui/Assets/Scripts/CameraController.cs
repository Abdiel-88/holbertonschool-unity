using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;        // Reference to the player's transform
    public Vector3 offset;          // Offset from the player
    public float rotationSpeed = 5.0f; // Speed of rotation
    public float smoothSpeed = 0.125f; // Speed of smoothing
    public bool isInverted = false; // Invert Y-axis

    private float yaw;        // Horizontal rotation
    private float pitch = 0f;  // Keep pitch neutral to avoid looking up or down

    void Start()
    {
        yaw = player.eulerAngles.y; // Align yaw to player's initial Y rotation
        Vector3 initialPosition = player.position + player.TransformDirection(offset);
        transform.position = initialPosition;
        transform.LookAt(player);
    }

    void LateUpdate()
    {
        // Check if the game is paused
        if (Time.timeScale == 0f)
        {
            return; // Do not allow camera movement when paused
        }

        // Get mouse input for rotation
        yaw += Input.GetAxis("Mouse X") * rotationSpeed;
        pitch -= Input.GetAxis("Mouse Y") * rotationSpeed * (isInverted ? -1 : 1);

        // Clamp the pitch to prevent the camera from rotating too far up or down
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
}
