using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public PlayerData playerData;
    [SerializeField]
    private string saveFilePath;

    void Start()
    {
        playerData = new PlayerData();

        saveFilePath = "";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadGame();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            //DeleteSaveFile();
        }
    }

    public void setSaveFilePath(string saveNumber)
    {
        saveFilePath = Application.persistentDataPath + "/PlayerData" + saveNumber + ".json";
    }

    public void SaveGame()
    {
        GetValue();

        string savePlayerData = JsonUtility.ToJson(playerData);
        File.WriteAllText(saveFilePath, savePlayerData);

        Debug.Log("Save file created at: " + saveFilePath);

        saveFilePath = "";
    }

    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string loadPlayerData = File.ReadAllText(saveFilePath);
            playerData = JsonUtility.FromJson<PlayerData>(loadPlayerData);

            Debug.Log("Load game complete!");

            //SetValue();
        }
        else
        {
            Debug.Log("There is no save files to load!");
        }

        
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

    public void GetValue()
    {
        playerData.lastCheckPointpos = GameManager.instance.lastCheckPointPos;
    }

    public void SetValue()
    {
        GameManager.instance.lastCheckPointPos = playerData.lastCheckPointpos;
    }
}

[System.Serializable]
public class PlayerData
{
    public Vector2 lastCheckPointpos;

}
