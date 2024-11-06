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

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private bool canControlInAir = true;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        startPosition = transform.position;
    }

    void Update()
    {
        if (Time.timeScale == 0f) return; // Prevent movement when paused

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 desiredMoveDirection = (forward * moveVertical + right * moveHorizontal).normalized;

        if (controller.isGrounded)
        {
            moveDirection = desiredMoveDirection * speed;
            canControlInAir = true;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpForce;
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

        controller.Move(moveDirection * Time.deltaTime);

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
        startPosition = transform.position; // Reset start position for respawn
    }
}
