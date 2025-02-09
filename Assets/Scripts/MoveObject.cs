using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MoveObject : MonoBehaviour
{
    [Header("Camera Variables")]
    private Camera mainCamera;
    private float cameraZDistance;

    [Header("Object Variables")]
    private Vector3 originalPos;
    private Quaternion originalRot;
    private bool isDragging = false;
    private bool isOnPlatform = false;

    [Header("Platform Variables")]
    private Transform currentPlatform; // The platform this object is on
    public float yOffset = 0.5f;
    private static MoveObject objectOnPlatform = null; // Tracks which object is on the platform globally

    void Start()
    {
        mainCamera = Camera.main; // Assign the main camera

        cameraZDistance = mainCamera.WorldToScreenPoint(transform.position).z; // Calculate the distance from the camera to the object

        // Set the original position and rotation of the object
        originalPos = transform.position;
        originalRot = transform.rotation;
    }

    private void Update()
    {
        // If the object is being dragged, don't do anything past this point
        if (isDragging)
        {
            return;
        }

        if (isOnPlatform && currentPlatform != null)
        {
            // Keep the object on the platform
            transform.position = new Vector3(
                currentPlatform.position.x,
                currentPlatform.position.y + yOffset,
                currentPlatform.position.z
            );
        }
        else
        {
            // Reset position and rotation if not on platform
            transform.position = originalPos;
            transform.rotation = originalRot;
        }
    }

    private void OnMouseDrag()
    {
        // Get the mouse position and convert it to world space
        Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraZDistance);

        // Set the object's position to the mouse position
        Vector3 newPos = mainCamera.ScreenToWorldPoint(screenPos);
        transform.position = newPos;

        isDragging = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;

        if (isOnPlatform && currentPlatform != null)
        {
            // Ensure the object remains on the platform when released
            transform.position = new Vector3(currentPlatform.position.x, currentPlatform.position.y + yOffset, currentPlatform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            // Allow this object to land if no other object is on the platform
            if (objectOnPlatform == null || objectOnPlatform == this)
            {
                isOnPlatform = true;
                currentPlatform = other.transform;
                objectOnPlatform = this; // Set this object as the one on the platform

                // Snap it to the platform immediately
                transform.position = new Vector3(currentPlatform.position.x, currentPlatform.position.y + yOffset, currentPlatform.position.z);

                // Set the platform as the parent of the object
                this.transform.SetParent(currentPlatform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Platform") && currentPlatform == other.transform)
        {
            // Object is no longer on the platform, so revert to original position
            isOnPlatform = false;
            currentPlatform = null;
            this.transform.SetParent(null);

            // Release platform ownership if this object was on it
            if (objectOnPlatform == this)
            {
                objectOnPlatform = null;
            }
        }
    }
}
