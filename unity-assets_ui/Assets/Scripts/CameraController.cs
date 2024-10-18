using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;        // Reference to the player's transform
    public Vector3 offset;          // Offset from the player
    public float rotationSpeed = 5.0f; // Speed of rotation
    public float smoothSpeed = 0.125f; // Speed of smoothing

    private float yaw;        // Horizontal rotation
    private float pitch = 0f;  // Keep pitch neutral to avoid looking up or down

    void Start()
    {
        // Directly set the initial yaw to match the player's rotation
        yaw = player.eulerAngles.y; // Align yaw to player's initial Y rotation
        
        // Set the initial camera position directly behind the player
        Vector3 initialPosition = player.position + player.TransformDirection(offset);
        transform.position = initialPosition;

        // Ensure the camera is looking at the player
        transform.LookAt(player);
    }

    void LateUpdate()
    {
        // Get mouse input for rotation
        yaw += Input.GetAxis("Mouse X") * rotationSpeed;
        pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;

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
