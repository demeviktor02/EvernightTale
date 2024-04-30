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

    public List<string> resoulutionsList;
   
    List<Resolution> resolutionsNew = new List<Resolution>();
    public TMPro.TMP_Text resoultionText;

    public List<string> languages;
    public TMPro.TMP_Text languageText;

    public List<string> qualityes;
    public TMPro.TMP_Text qualityText;

    public Slider musicVolumeSlider;
    public Slider environmentVolumeSlider;

    public TMPro.TMP_Text playerSessionCountText;

    public string saveFilePath;
    public SettingsData settingsData;

    void OnLoadCallback(Scene scene, LoadSceneMode sceneMode)
    {
        Start();
    }


    void Start()
    {
        SceneManager.sceneLoaded += this.OnLoadCallback;

        saveFilePath = Application.persistentDataPath + "/PlayerData" + "Settings" + ".json";


        string SessionNumber = PlayerPrefs.GetString("unity.player_session_count");
        playerSessionCountText.text = SessionNumber;

        Resolution[] resolutionsFirst = Screen.resolutions;

        int firstResolutionData = 0;

        for (int i = 0; i < resolutionsFirst.Length; i++)
        {
            string option = resolutionsFirst[i].width + " x " + resolutionsFirst[i].height; //+ " @" + resolutionsFirst[i].refreshRate;
            if (resolutionsFirst[i].refreshRate == Screen.currentResolution.refreshRate)
            {
                resoulutionsList.Add(option);
                resolutionsNew.Add(resolutionsFirst[i]);
            }





            if (resolutionsFirst[i].width == Screen.currentResolution.width &&
            resolutionsFirst[i].height == Screen.currentResolution.height)
            {
                firstResolutionData = resolutionsNew.Count-1;
            }

        }

        if (Application.platform != RuntimePlatform.Android &&
            Application.platform != RuntimePlatform.IPhonePlayer)
        {


            if (PlayerPrefs.GetString("unity.player_session_count") == "1")
            {
                settingsData.environmentVolumeData = 0;
                settingsData.musicVolumeData = 0;
                settingsData.qualityData = 2;
                settingsData.resolutionData = firstResolutionData;


                SetEnvironmentVolume(settingsData.environmentVolumeData);
                SetMusicVolume(settingsData.musicVolumeData);
                SetQuality(settingsData.qualityData);
                SetResolution(settingsData.resolutionData);
                SetLanguage(settingsData.languageData);

                SaveOptions();
            }
            else
            {
                LoadOptions();

                SetEnvironmentVolume(settingsData.environmentVolumeData);
                SetMusicVolume(settingsData.musicVolumeData);
                SetQuality(settingsData.qualityData);
                SetResolution(settingsData.resolutionData);
                SetLanguage(settingsData.languageData);
            }
        }
    }


    public void SetPlayerSessionCountToZero()
    {
        PlayerPrefs.SetString("unity.player_session_count", "0");
        playerSessionCountText.text = PlayerPrefs.GetString("unity.player_session_count");
    }

    public void SetLanguage(int languageIndex)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageIndex];

        languageText.text = languages[languageIndex];

    }

    public void SwitchLanguage(bool IsNext)
    {
        if (IsNext == true && settingsData.languageData +1 != languages.Count)
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[settingsData.languageData + 1];
            settingsData.languageData += 1;
        }
        else if (IsNext == false && settingsData.languageData - 1 != -1)
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[settingsData.languageData - 1];
            settingsData.languageData -= 1;
        }

        languageText.text = languages[settingsData.languageData];



    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutionsNew[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen, Screen.currentResolution.refreshRate);

        resoultionText.text = resoulutionsList[resolutionIndex];
    }

    public void SwitchResolution(bool IsNext)
    {
        if (IsNext == true && settingsData.resolutionData + 1 != resoulutionsList.Count)
        {
            Resolution resolution = resolutionsNew[settingsData.resolutionData + 1];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen, Screen.currentResolution.refreshRate);
            settingsData.resolutionData += 1;
        }
        else if (IsNext == false && settingsData.resolutionData - 1 != -1)
        {
            Resolution resolution = resolutionsNew[settingsData.resolutionData - 1];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen, Screen.currentResolution.refreshRate);
            settingsData.resolutionData -= 1;
        }

        resoultionText.text = resoulutionsList[settingsData.resolutionData];



    }

    public void SetMusicVolume(float volume)
    {

        audioMixer.SetFloat("musicVolume", volume);

        musicVolumeSlider.value = volume;

        settingsData.musicVolumeData = volume;

    }

    public void SetEnvironmentVolume(float volume)
    {

        audioMixer.SetFloat("environmentVolume", volume);

        environmentVolumeSlider.value = volume;

        settingsData.environmentVolumeData = volume;

    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);

        qualityText.text = qualityes[qualityIndex];

    }

    public void SwitchQuality(bool IsNext)
    {
        if (IsNext == true && settingsData.qualityData + 1 != qualityes.Count)
        {
            QualitySettings.SetQualityLevel(settingsData.qualityData + 1);
            settingsData.qualityData += 1;
        }
        else if (IsNext == false && settingsData.qualityData - 1 != -1)
        {
            QualitySettings.SetQualityLevel(settingsData.qualityData - 1);
            settingsData.qualityData -= 1;
        }

        qualityText.text = qualityes[settingsData.qualityData];



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

            //Debug.Log("Load game complete!");


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
    public float musicVolumeData;
    public float environmentVolumeData;
    public int qualityData;
    public int resolutionData;
    public int languageData;
}
