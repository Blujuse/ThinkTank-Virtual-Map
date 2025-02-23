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

    // Stores if an object is currently on the platform
    [HideInInspector] public bool objectOnPlatform = false;
    private BoxCollider boxCol;

    private void Start()
    {
        boxCol = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        ObjectChecker();
    }

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

    private void ObjectChecker()
    {
        // Cheap way of making it so other objects cant be placed on the platform
        // Need the box collider for rotation logic, so cant use enable
        // Set it from a trigger to collider to stop the OnTriggerEnter logic from working
        if (objectOnPlatform)
        {
            boxCol.isTrigger = false;
        }
        else
        {
            boxCol.isTrigger = true;
        }
    }
}