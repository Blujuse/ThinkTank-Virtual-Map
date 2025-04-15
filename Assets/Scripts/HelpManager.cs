using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpManager : MonoBehaviour
{
    public Animator helpAnim;

    public void ShowHelp()
    {
        StartCoroutine(HelpShower());
    }

    IEnumerator HelpShower()
    {
        helpAnim.SetInteger("HelpState", 1);

        yield return new WaitForSeconds(5f);

        helpAnim.SetInteger("HelpState", 2);
    }
}
