using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;  // The Player GameObject that the camera follows

    private Vector3 offset;    // The initial offset between the camera and the player

    // Start is called before the first frame update
    void Start()
    {
        // Calculate and store the offset by subtracting the player's position from the camera's position
        offset = transform.position - player.transform.position;
    }

    // LateUpdate is called after all Update functions have been called
    void LateUpdate()
    {
        // Set the camera's position to be the player's position plus the offset
        transform.position = player.transform.position + offset;
    }
}
