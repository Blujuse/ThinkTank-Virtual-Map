using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [Header("Camera Variables")]
    private Camera mainCamera;
    private float cameraZDistance;

    [Header("Object Variables")]
    private Rigidbody rb;
    private Vector3 originalPos;
    private Quaternion originalRot;
    public float yOffset;

    [Header("Platform Variables")]
    private GameObject platform;
    private SpinningPlatform platformScript; // Used to access if there is object on the platform
    private Animator platformAnim;
    private Transform platformTransform; // The platform this object is on
    public bool isOnPlatform;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main; // Assign the main camera
        if (mainCamera == null) // Check if camera is found
        {
            Debug.LogError("No camera found");
            return;
        }

        cameraZDistance = mainCamera.WorldToScreenPoint(transform.position).z; // Calculate the distance from the camera to the object

        rb = GetComponent<Rigidbody>(); // Getting rigidbody
        if (rb == null) // Check if rigidbody is found
        {
            Debug.LogError("No rigidbody found");
            return;
        }

        // Get platform
        platform = GameObject.FindGameObjectWithTag("Platform");
        if (platform == null) // Check if platform is found
        {
            Debug.LogError("No platform found");
            // No return for testing object moving and returning to spawn location
        }
        else
        {
            platformScript = platform.GetComponent<SpinningPlatform>();
            platformAnim = platform.GetComponent<Animator>();
        }

        // Set the original position and rotation of the object
        originalPos = transform.position;
        originalRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnPlatform)
        {
            // Snap it to the platform immediately
            transform.position = new Vector3(platformTransform.position.x, platformTransform.position.y + yOffset, platformTransform.position.z);

            // set these to trye for spinning and stuff
            platformScript.objectOnPlatform = true;
            rb.freezeRotation = true;

            platformAnim.SetBool("objectGrabbed", true);
        }
    }

    private void OnMouseDown()
    {
        // Make platform appear when an object is grabbed
        platformAnim.SetBool("objectGrabbed", true);

        // Set to false, to let model be moved off platform
        isOnPlatform = false;
        
        if (platformScript != null)
        {
            platformScript.objectOnPlatform = false;
        }
        
        // Remove child to stop it getting rotated when not on platform
        if (this.transform.parent != null)
        {
            this.transform.SetParent(null);
        }
    }

    private void OnMouseDrag()
    {
        // Get the mouse position and convert it to world space
        Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraZDistance);

        // Set the object's position to the mouse position
        Vector3 newPos = mainCamera.ScreenToWorldPoint(screenPos);
        transform.position = newPos;
    }

    private void OnMouseUp()
    {
        // Reset position and rotation if not on platform
        transform.position = originalPos;
        transform.rotation = originalRot;

        // Make platform disappear when an object is dropped
        if (!isOnPlatform)
        {
            platformAnim.SetBool("objectGrabbed", false);
        }
    }

    // When in trigger of platform move the car onto it
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            Debug.Log("Placed");

            platformTransform = other.transform;

            isOnPlatform = true;

            // Set the platform as the parent of the object
            this.transform.SetParent(platformTransform);
        }
    }
}
