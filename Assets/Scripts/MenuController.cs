using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject loadingScreen;
    public Image loadingBar;

    // Load back to the menu
    public void BackToTheMenu()
    {
        SceneManager.LoadScene("VirtualMap");
    }

    // Load the car scene
    public void LoadCarVille()
    {
        SceneManager.LoadScene("CarVille");
    }

    // Load the plane scene
    public void LoadPlaneCity()
    {
        SceneManager.LoadScene("PlaneCity");
    }

    // Load the train scene
    public void LoadTrainYard()
    {
        SceneManager.LoadScene("TrainYard");
    }

    public void LoadScene(int sceneIndex)
    {
        if (loadingScreen == null)
        {
            Debug.LogError("Loading screen not assigned.");
            return;
        }
        else
        {
            StartCoroutine(LoadSceneAsync(sceneIndex));
        }
    }

    IEnumerator LoadSceneAsync(int sceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);

            loadingBar.fillAmount = progress;

            yield return null;
        }
    }
}
