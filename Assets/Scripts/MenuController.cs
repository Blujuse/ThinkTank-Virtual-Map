using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
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
}
