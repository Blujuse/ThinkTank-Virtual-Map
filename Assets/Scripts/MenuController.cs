using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void BackToTheMenu()
    {
        SceneManager.LoadScene("VirtualMap");
    }

    public void LoadCarVille()
    {
        SceneManager.LoadScene("CarVille");
    }

    public void LoadPlaneCity()
    {
        SceneManager.LoadScene("PlaneCity");
    }

    public void LoadTrainYard()
    {
        SceneManager.LoadScene("TrainYard");
    }
}
