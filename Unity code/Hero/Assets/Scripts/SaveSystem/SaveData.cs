using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveData : MonoBehaviour
{
    public static SaveData instance;

    public PlayerData playerData;
    [SerializeField]
    public string saveFilePath;
    public string imageSaveFilePath;
    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        playerData = new PlayerData();

    }


    public void SaveGame()
    {
        GetValue();

        string savePlayerData = JsonUtility.ToJson(playerData);
        File.WriteAllText(saveFilePath, savePlayerData);

        Debug.Log("Save file created at: " + saveFilePath);

        saveFilePath = "";
    }


    public void GetValue()
    {
        playerData.LevelIndex = SceneManager.GetActiveScene().buildIndex;
        playerData.PlayTime += GameManager.instance.sessionTime;
    }
   

}

[System.Serializable]
public class PlayerData
{
    public int LevelIndex;
    public int Jumps;
    public int SlayedEnemies;
    public int Defeats;
    public float PlayTime;
    public int Difficulty;
}
