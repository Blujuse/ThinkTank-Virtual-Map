using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpManager : MonoBehaviour
{
    public Animator helpAnim;
    public Sprite AudioOnSprite;
    public Sprite AudioOffSprite;
    public Image helpButtonImage;
    private static bool isAudioMuted = false;

    private void Update()
    {
        if (isAudioMuted == false)
        {
            AudioListener.volume = 1;
            helpButtonImage.sprite = AudioOnSprite;
        }
        else if (isAudioMuted == true)
        {
            AudioListener.volume = 0;
            helpButtonImage.sprite = AudioOffSprite;
        }
    }

    public void ShowHelp()
    {
        StartCoroutine(HelpShower());
    }

    public void AudioMute()
    {
        if (isAudioMuted == true)
        {
            isAudioMuted = false;
        }
        else if (isAudioMuted == false)
        {
            isAudioMuted = true;
        }
    }

    IEnumerator HelpShower()
    {
        helpAnim.SetInteger("HelpState", 1);

        yield return new WaitForSeconds(5f);

        helpAnim.SetInteger("HelpState", 2);
    }
}
