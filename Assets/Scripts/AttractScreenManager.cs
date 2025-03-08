using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AttractScreenManager : MonoBehaviour
{
    bool input;
    float timer;
    [SerializeField] GameObject AttractScreen;
    private void Start()
    {
        AttractScreen.SetActive(false);
    }
    private void Update()
    {
        input = false;

        if (Input.anyKey || Input.anyKeyDown || Input.mousePosition != new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0))
        {
            input = true;
        }
        if (timer >= 5)
        {
            timer = 0;
            StartCoroutine(timeout());
        }

        if(!input)
        {
            timer += Time.deltaTime;
        }
    }

    IEnumerator timeout()
    {
        yield return new WaitForSeconds(5);
        AttractScreen.SetActive(true);
    }
}
