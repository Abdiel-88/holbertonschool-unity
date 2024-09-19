using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    void Update()
    {
        // Rotate the coin over time
        float rotationSpeed = 45f;
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
    }
}
