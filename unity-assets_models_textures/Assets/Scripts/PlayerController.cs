using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpForce = 8.0f;
    public float gravity = 20.0f;
    public Transform cameraTransform;  // Reference to the camera's transform

    public Vector3 startPosition;  // Starting position to respawn the player
    public float fallThreshold = -10.0f;  // Y position below which the player will respawn
    public float respawnHeight = 50.0f;  // Height above the starting position to respawn from

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        // Set the player's start position
        startPosition = transform.position;
    }

    void Update()
    {
        // Check if the player is grounded
        if (controller.isGrounded)
        {
            // Get input for movement
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            // Get the camera's forward and right directions
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;

            // Ignore the y component (we don't want to tilt the movement)
            forward.y = 0f;
            right.y = 0f;

            // Normalize directions
            forward.Normalize();
            right.Normalize();

            // Calculate movement direction relative to the camera
            moveDirection = (forward * moveVertical + right * moveHorizontal).normalized * speed;

            // Jump
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }

        // Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the player
        controller.Move(moveDirection * Time.deltaTime);

        // Check if the player falls below the fallThreshold
        if (transform.position.y < fallThreshold)
        {
            RespawnFromSky();
        }
    }

    void RespawnFromSky()
    {
        // Move the player to a position high above the starting point
        transform.position = new Vector3(startPosition.x, startPosition.y + respawnHeight, startPosition.z);

        // Completely reset vertical movement to ensure no falling momentum is carried over
        moveDirection = Vector3.zero;
    }
}
