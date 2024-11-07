using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 6.0f;
    public float airControlMultiplier = 0.5f;
    public float jumpForce = 8.0f;
    public float gravity = 20.0f;
    public Transform cameraTransform;
    
    public Vector3 startPosition;
    public float fallThreshold = -10.0f;
    public float respawnHeight = 50.0f;

    public float rotationSpeed = 15.0f; // New rotation speed variable for faster turning

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private Animator animator; // New reference to Animator component
    private bool canControlInAir = true;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>(); // Get Animator from the child model
        startPosition = transform.position;
    }

    void Update()
    {
        if (Time.timeScale == 0f) return; // Prevent movement when paused

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Get the forward and right directions based on the camera's orientation
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f; // Flatten the forward direction to the horizontal plane
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        // Determine the desired movement direction based on input and camera orientation
        Vector3 desiredMoveDirection = (forward * moveVertical + right * moveHorizontal).normalized;

        if (controller.isGrounded)
        {
            moveDirection = desiredMoveDirection * speed;
            canControlInAir = true;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpForce;
            }

            // Rotate the player to face the movement direction
            if (desiredMoveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(desiredMoveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }

            // Update the isRunning parameter in the Animator based on movement
            if (animator != null)
            {
                bool isMoving = desiredMoveDirection.magnitude > 0;
                animator.SetBool("isRunning", isMoving); // Set to true if moving, false if stationary
            }
        }
        else
        {
            if (canControlInAir)
            {
                Vector3 airMove = desiredMoveDirection * speed * airControlMultiplier;
                moveDirection.x = airMove.x;
                moveDirection.z = airMove.z;
            }

            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the player based on calculated moveDirection
        controller.Move(moveDirection * Time.deltaTime);

        // Respawn if the player falls below a certain height
        if (transform.position.y < fallThreshold)
        {
            RespawnFromSky();
        }
    }

    void RespawnFromSky()
    {
        transform.position = new Vector3(startPosition.x, startPosition.y + respawnHeight, startPosition.z);
        moveDirection = Vector3.zero;
        canControlInAir = false;
    }

    public void ResetPlayer()
    {
        moveDirection = Vector3.zero;
        canControlInAir = true;
        startPosition = transform.position; // Reset start position for respawn.
    }
}
