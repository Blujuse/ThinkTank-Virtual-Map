using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    private bool isHeld;

    [Header("Platform Variables")]
    private GameObject platform;
    private SpinningPlatform platformScript; // Used to access if there is object on the platform
    private Animator platformAnim;
    private Transform platformTransform; // The platform this object is on
    public bool isOnPlatform;

    [Header("Menu Variables")]
    private GameObject menu;
    private Animator menuAnim;
    private GameObject vehicleHeaderObj;
    private GameObject vehicleDescObj;
    private TMP_Text vehicleHeader;
    private TMP_Text vehicleText;
    public string vehicleName;
    public string vehicleInfo;
    public Image vehicleImageCanvas;
    public Sprite vehicleImages;

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

            // The animator is used to change the state of the platform using objectState animation int
            // The order is as follows:
            // 1 - Platform appears, this happens when an object is grabbed
            // 2 - Move platform to the left, this happens when an object is placed on the platform
            // 3 - Move platform to the Center, this happens when an object is taken from the platform
            // 4 - Platform disappears, this happens when an object is dropped
        }

        // Get menu
        menu = GameObject.FindGameObjectWithTag("Menu");
        if (menu == null) // Check if menu is found
        {
            Debug.LogError("No menu found");
            return;
        }
        else
        {
            menuAnim = menu.GetComponent<Animator>();
        }

        // Get the text object
        vehicleHeaderObj = GameObject.FindGameObjectWithTag("Header");
        if (vehicleHeaderObj == null) // Check if text object is found
        {
            Debug.LogError("No text object found");
            return;
        }
        else
        {
            // Get the text mesh pro components
            vehicleHeader = vehicleHeaderObj.GetComponent<TMP_Text>();
        }

        // Get the text object
        vehicleDescObj = GameObject.FindGameObjectWithTag("Desc");
        if (vehicleDescObj == null) // Check if text object is found
        {
            Debug.LogError("No text object found");
            return;
        }
        else
        {
            // Get the text mesh pro components
            vehicleText = vehicleDescObj.GetComponent<TMP_Text>();
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

            // Make platform move to the left when object is placed on it
            platformAnim.SetInteger("objectState", 2);
            menuAnim.SetBool("infoOpen", true);

            // Set the text to car on platform
            vehicleHeader.SetText(vehicleName);
            vehicleText.SetText(vehicleInfo);

            vehicleImageCanvas.sprite = vehicleImages;
        }
    }

    private void OnMouseDown()
    {
        // Make platform appear when an object is grabbed
        platformAnim.SetInteger("objectState", 1);

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

        // Sets to true so the object is only placed when held
        isHeld = true;
    }

    private void OnMouseUp()
    {
        // Reset position and rotation if not on platform
        transform.position = originalPos;
        transform.rotation = originalRot;

        // Make platform disappear when an object is dropped
        if (!isOnPlatform)
        {
            platformAnim.SetInteger("objectState", 4);
            menuAnim.SetBool("infoOpen", false);
        }

        // Sets to false so the object is only placed when held
        isHeld = false;
    }

    // When in trigger of platform move the car onto it
    private void OnTriggerEnter(Collider other)
    {
        // Using isHeld to make sure object is only placed when it should be
        if (other.CompareTag("Platform") && isHeld)
        {
            Debug.Log("Placed");

            platformTransform = other.transform;

            isOnPlatform = true;

            // Set the platform as the parent of the object
            this.transform.SetParent(platformTransform);
        }
    }
}
