using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AttractScreenManager : MonoBehaviour
{
    [Header("User Stuff")]
    private bool input;
    Vector3 oldMousePos;

    [Header("UI Stuff")]
    public float timer;
    [SerializeField] private GameObject AttractScreen;
    private Coroutine timeoutCoroutine;

    private void Start()
    {

    }

    private void Update()
    {
        // Check for user inactivity
        if (Input.anyKey || Input.anyKeyDown || Input.mousePosition != oldMousePos)
        {
            input = true;
            //Debug.Log(Input.mousePosition.x + " / " + Input.mousePosition.y + " \n" + oldMousePos.x + " / " + oldMousePos.y);

            if (timeoutCoroutine != null)
            {
                StopCoroutine(timeoutCoroutine);
            }
        }
        else
        {
            input = false;
        }

        //Debug.Log(input);

        // If there is no input start timer countdown
        if (!input)
        {
            timeoutCoroutine = StartCoroutine(timeout());
            //Debug.Log("Start Timeout");
        }

        oldMousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
    }

    IEnumerator timeout()
    {
        float elapsedTime = 0f;

        // Continue checking for inactivity over time
        while (elapsedTime < 5f)
        {
            if (input)
            {
                yield break; // Exit the coroutine if input is detected
            }

            elapsedTime += Time.deltaTime; // Accumulate time passed
            yield return null;
        }

        if (SceneManager.GetActiveScene().name != "Attract_Screen")
        {
            SceneManager.LoadScene("Attract_Screen"); // Set false at beginning of scenes that are not the Attract_Screen scene
        }
    }
}
