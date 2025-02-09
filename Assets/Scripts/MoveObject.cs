using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]

public class MoveObject : MonoBehaviour
{
    // Stores the main camera
    private Camera mainCamera;

    // Stores the distance between the camera and the object
    private float cameraZDistance;

    private Vector3 originalPos;

    // Start is called before the first frame update
    void Start()
    {
        // Set the mainCamera variable to the main camera in the scene
        mainCamera = Camera.main;

        cameraZDistance = mainCamera.WorldToScreenPoint(transform.position).z;

        originalPos = transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            transform.position = originalPos;
        }
    }

    private void OnMouseDrag()
    {
        Vector3 ScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraZDistance);

        Vector3 NewPos = mainCamera.ScreenToWorldPoint(ScreenPos);

        transform.position = NewPos;
    }
}
