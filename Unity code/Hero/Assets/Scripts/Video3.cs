using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Video3 : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject videoSkip;
    public bool inCutscene;
    public string engVideoLink = "https://demeviktor02.github.io/EvernightTaleVideo/end_eng.mp4";
    public string hunVideoLink = "https://demeviktor02.github.io/EvernightTaleVideo/end_hun.mp4";



    // Update is called once per frame
    void Update()
    {
        if ((videoPlayer.frame) > 0 && (videoPlayer.isPlaying == false))
        {
            videoSkip.SetActive(false);

            GameManager.instance.inGame = false;
            AudioManager.instance.UnMute("Village");
            AudioManager.instance.UnMute("VillageLoop");
            AudioManager.instance.UnMute("Trigger");
            StartCoroutine(setInCutsceneFalse());
        }


        if (Input.GetButtonDown("Cancel") && inCutscene == true)
        {
            videoSkip.SetActive(false);

            GameManager.instance.inGame = false;
            AudioManager.instance.UnMute("Village");
            AudioManager.instance.UnMute("VillageLoop");
            AudioManager.instance.UnMute("Trigger");
            StartCoroutine(setInCutsceneFalse());
        }
    }

    public IEnumerator setInCutsceneFalse()
    {
        GameManager.instance.inCutscene = false;
        inCutscene = false;
        SceneManager.LoadScene("Menu");
        
        yield return null;
    }

    public void PlayVideo()
    {
        Locale currentSelectedLocale = LocalizationSettings.SelectedLocale;
        ILocalesProvider availableLocales = LocalizationSettings.AvailableLocales;

        if (currentSelectedLocale == availableLocales.GetLocale("en"))//settingsMenu.settingsData.languageData == 0)
        {
            videoPlayer.url = engVideoLink;
        }
        else
        {
            videoPlayer.url = hunVideoLink;
        }
        videoPlayer.Play();
        videoSkip.SetActive(true);
    }

    public void IsCutSceneOn()
    {
        GameManager.instance.inCutscene = true;
        inCutscene = true;
        AudioManager.instance.Mute("Village");
        AudioManager.instance.Mute("VillageLoop");
        AudioManager.instance.Mute("Trigger");
        AudioManager.instance.StopAudio("Village");
        AudioManager.instance.StopAudio("VillageLoop");
        AudioManager.instance.StopAudio("Trigger");
    }
}
