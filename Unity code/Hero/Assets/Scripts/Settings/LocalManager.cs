using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System;


public class LocalManager : MonoBehaviour
{
    public TMPro.TMP_Text Save1;
    public TMPro.TMP_Text Save2;
    public TMPro.TMP_Text Save3;

    public TMPro.TMP_Text SaveNumber;
    public TMPro.TMP_Text GamePercent;
    public TMPro.TMP_Text Jumps;
    public TMPro.TMP_Text Enemies;
    public TMPro.TMP_Text Deaths;
    public TMPro.TMP_Text GameTime;
    public GameObject LoadingScreen;

    private void Start()
    {
        ContinueOrNewGame();
    }

    public void ContinueOrNewGame()
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerData" + 1 + ".json"))
        {
            Save1.text = "CONTINUE";
        }
        else
        {
            Save1.text = "NEW GAME";
        }
        if (File.Exists(Application.persistentDataPath + "/PlayerData" + 2 + ".json"))
        {
            Save2.text = "CONTINUE";
        }
        else
        {
            Save2.text = "NEW GAME";
        }
        if (File.Exists(Application.persistentDataPath + "/PlayerData" + 3 + ".json"))
        {
            Save3.text = "CONTINUE";
        }
        else
        {
            Save3.text = "NEW GAME";
        }
    }

    public void setSaveFilePath(string saveNumber)
    {
        SaveData.instance.saveFilePath = Application.persistentDataPath + "/PlayerData" + saveNumber + ".json";
    }

    public void LoadFromSaveFilePath(string saveNumber)
    {
        SaveNumber.text = saveNumber;

        //Mentés útjának megadása
        SaveData.instance.saveFilePath = Application.persistentDataPath + "/PlayerData" + saveNumber + ".json";

        //Beolvasás
        string loadPlayerData = File.ReadAllText(SaveData.instance.saveFilePath);

        //Értékadás
        SaveData.instance.playerData = JsonUtility.FromJson<PlayerData>(loadPlayerData);

        SetValue();
    }

    public void LoadGame()
    {
        StartCoroutine(LoadSceneAsync());
    }

    public void SetValue()
    {
        GamePercent.text = (SaveData.instance.playerData.LevelIndex * 16.66).ToString() ;
        Jumps.text = SaveData.instance.playerData.Jumps.ToString();
        Enemies.text = SaveData.instance.playerData.SlayedEnemies.ToString();
        Deaths.text = SaveData.instance.playerData.Defeats.ToString();
        GameManager.instance.lastLevelIndex = SaveData.instance.playerData.LevelIndex;
    }

    public void DeleteSaveFile(string saveNumber)
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerData" + saveNumber + ".json"))
        {
            File.Delete(Application.persistentDataPath + "/PlayerData" + saveNumber + ".json");

            Debug.Log("Save file deleted!");

        }
        else
        {
            Debug.Log("There is nothing to delete!");
        }
    }

    IEnumerator LoadSceneAsync()
    {
        try
        {
            string loadPlayerData = File.ReadAllText(SaveData.instance.saveFilePath);
            SaveData.instance.playerData = JsonUtility.FromJson<PlayerData>(loadPlayerData);
            SetValue();
        }
        catch 
        {
            Debug.Log("There is no save files to load!");
            SaveData.instance.playerData.LevelIndex = 1;
        }


        //if (File.Exists(SaveData.instance.saveFilePath))
        //{
        //    Debug.Log("Save file exists");
        //    string loadPlayerData = File.ReadAllText(SaveData.instance.saveFilePath);
        //    SaveData.instance.playerData = JsonUtility.FromJson<PlayerData>(loadPlayerData);

        //    Debug.Log("Load game complete!");

        //    SetValue();


        //}
        //else
        //{
        //    SaveData.instance.playerData.LevelIndex = 1;
        //    Debug.Log("There is no save files to load!");
        //}

        AsyncOperation operation = SceneManager.LoadSceneAsync(SaveData.instance.playerData.LevelIndex);

        GameManager.instance.inGame = true;

        LoadingScreen.SetActive(true);

        while (!operation.isDone)
        {           
            yield return null;
        }
    }
}
