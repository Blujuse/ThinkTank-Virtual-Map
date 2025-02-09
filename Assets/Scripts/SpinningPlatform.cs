using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]

public class SpinningPlatform : MonoBehaviour
{
    // Rotation speed of the platform
    public float rotationSpeed = 5f;

    // Stores where the mouse was last
    private Vector3 lastMousePosition;

    // When mouse is dragged over the platform spin it
    private void OnMouseDrag()
    {
        // delta is the difference between the current and last mouse position
        Vector3 delta = Input.mousePosition - lastMousePosition;

        // Negative to match expected behavior otherwise it will rotate in the opposite direction
        float rotY = -delta.x * rotationSpeed;

        // Rotate around Y-axis of platform
        transform.Rotate(Vector3.up, rotY, Space.World);

        // Update last mouse position
        lastMousePosition = Input.mousePosition;
    }

    private void OnMouseDown()
    {
        // Initialize last position on mouse down
        lastMousePosition = Input.mousePosition;
    }
}
