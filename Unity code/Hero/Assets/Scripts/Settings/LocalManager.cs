using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System;
using Unity.Mathematics;
using UnityEngine.UI;
using TMPro;


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
    public TMPro.TMP_Text Difficulty;
    public Image image;
    public GameObject LoadingScreen;
    public Animator animator;

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

    public void ContinueOrNewGameNext(int saveNumber)
    {
        if (saveNumber == 1)
        {
            if (Save1.text == "NEW GAME")
            {
                animator.Play("SelectDifficultyOpen");
            }
            else
            {
                animator.Play("ContinueOpen");
            }
        }
        else if (saveNumber == 2)
        {
            if (Save2.text == "NEW GAME")
            {
                animator.Play("SelectDifficultyOpen");
            }
            else
            {
                animator.Play("ContinueOpen");
            }
        }
        else if (saveNumber == 3)
        {
            if (Save3.text == "NEW GAME")
            {
                animator.Play("SelectDifficultyOpen");
            }
            else
            {
                animator.Play("ContinueOpen");
            }
        }
        
    }

    public void LoadFromSaveFilePath(string saveNumber)
    {
        SaveNumber.text = saveNumber;

        //Mentés útjának megadása
        SaveData.instance.saveFilePath = Application.persistentDataPath + "/PlayerData" + saveNumber + ".json";
        SaveData.instance.imageSaveFilePath = Application.persistentDataPath + "/SaveImage" + saveNumber + ".png";

        //Beolvasás
        string loadPlayerData = File.ReadAllText(SaveData.instance.saveFilePath);

        //Értékadás
        SaveData.instance.playerData = JsonUtility.FromJson<PlayerData>(loadPlayerData);

        SetValue();
    }

    public void LoadGame(int difficulty)
    {
        StartCoroutine(LoadSceneAsync(difficulty));
    }

    public void SetValue()
    {
        GamePercent.text = math.round(SaveData.instance.playerData.LevelIndex * 16.66).ToString() + "%";
        Jumps.text = SaveData.instance.playerData.Jumps.ToString();
        Enemies.text = SaveData.instance.playerData.SlayedEnemies.ToString();
        Deaths.text = SaveData.instance.playerData.Defeats.ToString();

        if (SaveData.instance.playerData.Difficulty == 0)
        {
            Difficulty.text = "Easy";
        }
        else if (SaveData.instance.playerData.Difficulty == 1)
        {
            Difficulty.text = "Normal";
        }
        else if (SaveData.instance.playerData.Difficulty == 2)
        {
            Difficulty.text = "Hard";
        }
        

        TimeSpan time = TimeSpan.FromSeconds(SaveData.instance.playerData.PlayTime);
        GameTime.text = time.ToString(@"hh\:mm\:ss");
        Sprite loadImage = LoadSprite(SaveData.instance.imageSaveFilePath);
        image.sprite = loadImage;

        GameManager.instance.lastLevelIndex = SaveData.instance.playerData.LevelIndex;
        GameManager.instance.difficulty = SaveData.instance.playerData.Difficulty;
    }

    private Sprite LoadSprite(string path)
    {
        if (string.IsNullOrEmpty(path)) return null;
        if (System.IO.File.Exists(path))
        {
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(bytes);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            return sprite;
        }
        return null;
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

    IEnumerator LoadSceneAsync(int difficulty)
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
            SaveData.instance.playerData.Difficulty = difficulty;
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
