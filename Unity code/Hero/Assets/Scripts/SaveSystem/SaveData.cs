using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public PlayerData playerData;
    [SerializeField]
    private string saveFilePath;
    public TMPro.TMP_Text Save1;
    public TMPro.TMP_Text Save2;
    public TMPro.TMP_Text Save3;

    void Start()
    {
        playerData = new PlayerData();

        saveFilePath = "";

        ContinueOrNewGame();
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

            SetValue();
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

            ContinueOrNewGame();
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

}

[System.Serializable]
public class PlayerData
{
    public Vector2 lastCheckPointpos;

}
