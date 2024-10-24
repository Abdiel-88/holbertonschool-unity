using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float rotationSpeed = 5.0f;
    public float smoothSpeed = 0.125f;
    public bool isInverted = false; // Initial state

    private float yaw;
    private float pitch = 0f;

    void Start()
    {
        // Load the saved inverted state from PlayerPrefs
        isInverted = PlayerPrefs.GetInt("InvertY", 0) == 1;

        yaw = player.eulerAngles.y;
        Vector3 initialPosition = player.position + player.TransformDirection(offset);
        transform.position = initialPosition;
        transform.LookAt(player);
    }

    void LateUpdate()
    {
        if (PauseMenu.isPaused) return;

        // Get mouse input for rotation
        yaw += Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;
        pitch += isInverted ? mouseY : -mouseY;

        pitch = Mathf.Clamp(pitch, -30f, 45f);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 desiredPosition = player.position + rotation * offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        transform.LookAt(player);
    }
}
