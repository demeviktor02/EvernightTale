using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;


public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;   

    Resolution[] resolutions;

    public Toggle FullScreentoggle;

    public TMPro.TMP_Dropdown qualityDropdonw;

    public TMPro.TMP_Text resolutionText;

    public TMPro.TMP_Dropdown resolutionDropdown;

    public TMPro.TMP_Dropdown languageDropdown;

    public Slider volumeSlider;


    public TMPro.TMP_Text text;

    public string saveFilePath;
    public SettingsData settingsData;
    void Start()
    {
        saveFilePath = Application.persistentDataPath + "/PlayerData" + "Settings" + ".json";

        if (Application.platform == RuntimePlatform.WebGLPlayer || Application.platform == RuntimePlatform.Android ||
            Application.platform == RuntimePlatform.IPhonePlayer)
        {
            FullScreentoggle.gameObject.SetActive(false);
            resolutionText.gameObject.SetActive(false);
            resolutionDropdown.gameObject.SetActive(false);
        }

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


        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();

        if (PlayerPrefs.GetString("unity.player_session_count") == "1")
        {
            settingsData.volumeData = 0;
            settingsData.graphicsData = 2;
            settingsData.isFullScreenData = true;
            settingsData.resolutionData = firstResolutionData;


            SetVolume(settingsData.volumeData);
            SetQuality(settingsData.graphicsData);
            SetFullScreen(settingsData.isFullScreenData);
            SetResolution(settingsData.resolutionData);
            SetLanguage(settingsData.languageData);

            SaveOptions();
        }
        else
        {
            LoadOptions();

            SetVolume(settingsData.volumeData);
            SetQuality(settingsData.graphicsData);
            SetFullScreen(settingsData.isFullScreenData);
            SetResolution(settingsData.resolutionData);
            SetLanguage(settingsData.languageData);
        } 
    }

    public void SetLanguage(int languageIndex)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageIndex];

        languageDropdown.value = languageIndex;

        settingsData.languageData = languageIndex;

        SaveOptions();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        resolutionDropdown.value = resolutionIndex;

        settingsData.resolutionData = resolutionIndex;

        SaveOptions();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);

        volumeSlider.value = volume;

        settingsData.volumeData = volume;

        SaveOptions();
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);

        qualityDropdonw.value = qualityIndex;

        settingsData.graphicsData = qualityIndex;

        SaveOptions();
    }

    public void SetFullScreen(bool isFullscreen)
    {
        //Screen.SetResolution(resolutions[resolutionData].width, resolutions[resolutionData].height, isFullscreen);
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, isFullscreen);

        FullScreentoggle.isOn = isFullscreen;

        settingsData.isFullScreenData = isFullscreen;

        SaveOptions();
    }

    public void SaveOptions()
    {

        string saveSettingsData = JsonUtility.ToJson(settingsData);
        File.WriteAllText(saveFilePath, saveSettingsData);

        Debug.Log("Settings save file created at: " + saveFilePath);

    }


    public void LoadOptions()
    {

        if (File.Exists(saveFilePath))
        {
            string loadPlayerData = File.ReadAllText(saveFilePath);
            settingsData = JsonUtility.FromJson<SettingsData>(loadPlayerData);

            Debug.Log("Load game complete!");


        }
        else
        {
            Debug.Log("There is no load!");
        }

    }


}

[System.Serializable]
public class SettingsData
{
    public float volumeData;
    public int graphicsData;
    public bool isFullScreenData;
    public int resolutionData;
    public int languageData;
}
