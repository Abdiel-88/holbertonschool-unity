using System.Collections;
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

    public float rotationSpeed = 15.0f;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private Animator animator;
    private bool isFalling = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        startPosition = transform.position;
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;

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
            if (isFalling)
            {
                animator.SetBool("isFalling", false);
                isFalling = false; // Reset falling state when grounded
            }

            moveDirection = desiredMoveDirection * speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpForce;
                animator.SetTrigger("Jump");
            }

            if (desiredMoveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(desiredMoveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }

            animator.SetBool("isRunning", desiredMoveDirection.magnitude > 0);
        }
        else
        {
            // Enable falling state only when initially leaving the ground
            if (moveDirection.y < 0 && !isFalling)
            {
                animator.SetBool("isFalling", true);
                isFalling = true;
            }

            if (isFalling)
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
        isFalling = false;
        animator.SetBool("isFalling", false);
    }

    public void ResetPlayer()
    {
        moveDirection = Vector3.zero;
        isFalling = false;
        transform.position = startPosition;
        animator.SetBool("isFalling", false);
        animator.SetBool("isRunning", false);
    }
}
