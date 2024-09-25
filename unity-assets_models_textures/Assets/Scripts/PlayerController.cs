using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpForce = 8.0f;
    public float gravity = 20.0f;

    public Transform cameraTransform;  // Reference to the camera's transform

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Check if the player is grounded
        if (controller.isGrounded)
        {
            // Get the input for movement
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            // Get the camera's forward and right directions
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;

            // Ignore the y component (we don't want to tilt the movement)
            forward.y = 0f;
            right.y = 0f;
            
            // Normalize the directions
            forward.Normalize();
            right.Normalize();

            // Calculate the movement direction relative to the camera
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
    }
}
