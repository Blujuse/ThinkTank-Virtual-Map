using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsController : MonoBehaviour
{
    public GameObject creditsPanel;
    private bool isCreditsActive = false;

    public void ShowCredits()
    {
        if (isCreditsActive == false)
        {
            creditsPanel.SetActive(true);
            isCreditsActive = true;
        }
        else if (isCreditsActive == true)
        {
            creditsPanel.SetActive(false);
            isCreditsActive = false;
        }
    }
}