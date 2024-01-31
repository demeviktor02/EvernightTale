using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;


public class LocalManager : MonoBehaviour
{
    public TMPro.TMP_Text Save1;
    public TMPro.TMP_Text Save2;
    public TMPro.TMP_Text Save3;

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

    public void LoadGame()
    {

        if (File.Exists(SaveData.instance.saveFilePath))
        {
            string loadPlayerData = File.ReadAllText(SaveData.instance.saveFilePath);
            SaveData.instance.playerData = JsonUtility.FromJson<PlayerData>(loadPlayerData);

            Debug.Log("Load game complete!");

            SetValue();

            
        }
        else
        {
            SaveData.instance.playerData.LevelIndex = 1;
            Debug.Log("There is no save files to load!");
        }

        GameManager.instance.inGame = true;
        SceneManager.LoadScene(SaveData.instance.playerData.LevelIndex);
    }

    public void SetValue()
    {
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
}
