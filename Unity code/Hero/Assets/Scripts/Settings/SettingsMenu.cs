using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;   

    Resolution[] resolutions;

    public Toggle FullScreentoggle;

    public TMPro.TMP_Dropdown qualityDropdonw;

    public TMPro.TMP_Dropdown resolutionDropdown;

    public Slider volumeSlider;


    public float volumeData = -80;
    public int graphicsData = 2;
    public bool isFullScreenData = false;
    public int resolutionData = 0;

    public TMPro.TMP_Text text;

    
    void Start()
    {
        string SessionNumber = PlayerPrefs.GetString("unity.player_session_count");
        Debug.Log(SessionNumber);
        text.text = SessionNumber;

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int firstResolutionData = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            
                if (resolutions[i].width == Screen.width &&
                resolutions[i].height == Screen.height)
                {
                    firstResolutionData = i;
                }
                      
        }

        //SetResolution(firstResolutionData);

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();

        //if (PlayerPrefs.GetString("unity.player_session_count") == "1")
        //{
        //    volumeData = -80;
        //    graphicsData = 2;
        //    isFullScreenData = true;
        //    resolutionData = firstResolutionData;


        //    SetVolume(volumeData);
        //    SetQuality(graphicsData);
        //    SetFullScreen(isFullScreenData);
        //    SetResolution(resolutionData);

        //}
        //else
        //{

        //    SetVolume(volumeData);
        //    SetQuality(graphicsData);
        //    SetFullScreen(isFullScreenData);
        //    SetResolution(resolutionData);
        //} 
    }

    public void setLaungage(int laungageIndex)
    {
        //yield return LocalizationSettings.InitializationOperation; IENUMERATOR

        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[laungageIndex];
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        resolutionDropdown.value = resolutionIndex;

        resolutionData = resolutionIndex;

    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);

        volumeSlider.value = volume;

        volumeData = volume;

    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);

        qualityDropdonw.value = qualityIndex;

        graphicsData = qualityIndex;

    }

    public void SetFullScreen(bool isFullscreen)
    {
        //Screen.SetResolution(resolutions[resolutionData].width, resolutions[resolutionData].height, isFullscreen);
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, isFullscreen);

        FullScreentoggle.isOn = isFullscreen;

        isFullScreenData = isFullscreen;

        if (Application.platform == RuntimePlatform.Android)
        {
            Screen.SetResolution(Screen.width, Screen.height, isFullscreen);
        }

    }

}
