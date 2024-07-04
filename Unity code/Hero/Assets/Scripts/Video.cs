using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;
using UnityEngine.Video;
using static System.Net.WebRequestMethods;

public class Video : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Animator animator;
    public NPCMenu NPCMenu;
    public bool InCutScene;
    public string engVideoLink = "https://demeviktor02.github.io/EvernightTaleVideo/intro_eng.mp4";
    public string hunVideoLink = "https://demeviktor02.github.io/EvernightTaleVideo/intro_hun.mp4";


    // Update is called once per frame
    void Update()
    {
        if ((videoPlayer.frame) > 0 && (videoPlayer.isPlaying == false))
        {
            videoPlayer.Stop();
            animator.Play("EndCutscene");
        }
        

        if (Input.GetButton("Cancel") && InCutScene == true)
        {
            videoPlayer.Stop();
            animator.Play("EndCutscene");
        }
    }

    public void PlayVideo()
    {        
        videoPlayer.Play();
    }

    public void StartNpcTalking()
    {
        NPCMenu.startBool = true;
    }

    public void IsCutSceneOn()
    {
        InCutScene = true;
    }

    public void SetVideoClip()
    {
        Locale currentSelectedLocale = LocalizationSettings.SelectedLocale;
        ILocalesProvider availableLocales = LocalizationSettings.AvailableLocales;

        if (currentSelectedLocale == availableLocales.GetLocale("en"))
        {
            videoPlayer.url = engVideoLink;
        }
        else
        {
            videoPlayer.url = hunVideoLink;
        }
    }
}
