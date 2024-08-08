using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;
using UnityEngine.Video;

public class Video2 : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject videoSkip;
    public bool inCutscene;
    public NPC NPC;
    //public string engVideoLink = "https://demeviktor02.github.io/EvernightTaleVideo/lady_eng.mp4";
    //public string hunVideoLink = "https://demeviktor02.github.io/EvernightTaleVideo/lady_hun.mp4";
    public VideoClip engVideo;
    public VideoClip hunVideo;

    public GameObject rawimage;


    // Update is called once per frame
    void Update()
    {
        if ((videoPlayer.frame) > 0 && (videoPlayer.isPlaying == false))
        {
            videoSkip.SetActive(false);
            videoPlayer.Stop();

            NPC.NPCCam.SetActive(false);
            AudioManager.instance.UnMute("Village");
            AudioManager.instance.UnMute("VillageLoop");
            //AudioManager.instance.UnMute("Trigger");
            StartCoroutine(setInCutsceneFalse());
            gameObject.GetComponent<Animator>().Play("End");
        }


        if (Input.GetButton("Cancel") && inCutscene == true || Input.touchCount > 0 && inCutscene == true)
        {
            videoSkip.SetActive(false);
            videoPlayer.Stop();

            NPC.NPCCam.SetActive(false);
            AudioManager.instance.UnMute("Village");
            AudioManager.instance.UnMute("VillageLoop");
            //AudioManager.instance.UnMute("Trigger");
            StartCoroutine(setInCutsceneFalse());
            gameObject.GetComponent<Animator>().Play("End");

        }
    }

    public IEnumerator setInCutsceneFalse()
    {
        yield return new WaitForSeconds(1f);
        inCutscene = false;
        GameManager.instance.inCutscene = false;
    }

    public void PlayVideo()
    {
        videoPlayer.Play();
        videoSkip.SetActive(true);
    }

    public void IsCutSceneOn()
    {
        GameManager.instance.inCutscene = true;
        inCutscene = true;
        AudioManager.instance.Mute("Village");
        AudioManager.instance.Mute("VillageLoop");
    }

    public void SetVideoClip()
    {
        Locale currentSelectedLocale = LocalizationSettings.SelectedLocale;
        ILocalesProvider availableLocales = LocalizationSettings.AvailableLocales;

        if (currentSelectedLocale == availableLocales.GetLocale("en"))
        {
            //videoPlayer.url = engVideoLink;
            videoPlayer.clip = engVideo;
        }
        else
        {
            //videoPlayer.url = hunVideoLink;
            videoPlayer.clip = hunVideo;
        }
    }
}
