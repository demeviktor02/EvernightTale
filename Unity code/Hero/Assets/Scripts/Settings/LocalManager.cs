using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using Unity.Mathematics;


public class LocalManager : MonoBehaviour
{
    public TMPro.TMP_Text Save1;  
    public TMPro.TMP_Text GamePercent1;
    public TMPro.TMP_Text GameTime1;
    public TMPro.TMP_Text Difficulty1;
    public TMPro.TMP_Text StartTime1;
    public Image Image1;

    public TMPro.TMP_Text Save2;
    public TMPro.TMP_Text GamePercent2;
    public TMPro.TMP_Text GameTime2;
    public TMPro.TMP_Text Difficulty2;
    public TMPro.TMP_Text StartTime2;
    public Image Image2;

    public TMPro.TMP_Text Save3;
    public TMPro.TMP_Text GamePercent3;
    public TMPro.TMP_Text GameTime3;
    public TMPro.TMP_Text Difficulty3;
    public TMPro.TMP_Text StartTime3;
    public Image Image3;

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

            //Mentés útjának megadása
            SaveData.instance.saveFilePath = Application.persistentDataPath + "/PlayerData" + 1 + ".json";
            SaveData.instance.imageSaveFilePath = Application.persistentDataPath + "/SaveImage" + 1 + ".png";

            //Beolvasás
            string loadPlayerData1 = File.ReadAllText(SaveData.instance.saveFilePath);

            //Értékadás
            SaveData.instance.playerData = JsonUtility.FromJson<PlayerData>(loadPlayerData1);

            SetValue1();
        }
        else
        {
            Save1.text = "NEW GAME";
            GamePercent1.text = "0%";
            GameTime1.text = "00:00:00";
            Difficulty1.text = "-";
            StartTime1.text = "-";
            Image1 = null;
        }
        if (File.Exists(Application.persistentDataPath + "/PlayerData" + 2 + ".json"))
        {
            Save2.text = "CONTINUE";

            //Mentés útjának megadása
            SaveData.instance.saveFilePath = Application.persistentDataPath + "/PlayerData" + 2 + ".json";
            SaveData.instance.imageSaveFilePath = Application.persistentDataPath + "/SaveImage" + 2 + ".png";

            //Beolvasás
            string loadPlayerData2 = File.ReadAllText(SaveData.instance.saveFilePath);

            //Értékadás
            SaveData.instance.playerData = JsonUtility.FromJson<PlayerData>(loadPlayerData2);

            SetValue2();
        }
        else
        {
            Save2.text = "NEW GAME";
            GamePercent2.text = "0%";
            GameTime2.text = "00:00:00";
            Difficulty2.text = "-";
            StartTime2.text = "-";
            Image2 = null;
        }
        if (File.Exists(Application.persistentDataPath + "/PlayerData" + 3 + ".json"))
        {
            Save3.text = "CONTINUE";

            //Mentés útjának megadása
            SaveData.instance.saveFilePath = Application.persistentDataPath + "/PlayerData" + 3 + ".json";
            SaveData.instance.imageSaveFilePath = Application.persistentDataPath + "/SaveImage" + 3 + ".png";

            //Beolvasás
            string loadPlayerData3 = File.ReadAllText(SaveData.instance.saveFilePath);

            //Értékadás
            SaveData.instance.playerData = JsonUtility.FromJson<PlayerData>(loadPlayerData3);

            SetValue3();
        }
        else
        {
            Save3.text = "NEW GAME";
            GamePercent3.text = "0%";
            GameTime3.text = "00:00:00";
            Difficulty3.text = "-";
            StartTime3.text = "-";
            Image3 = null;
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
        //Mentés útjának megadása
        SaveData.instance.saveFilePath = Application.persistentDataPath + "/PlayerData" + saveNumber + ".json";
        SaveData.instance.imageSaveFilePath = Application.persistentDataPath + "/SaveImage" + saveNumber + ".png";

        //Beolvasás
        //string loadPlayerData = File.ReadAllText(SaveData.instance.saveFilePath);

        //Értékadás
        //SaveData.instance.playerData = JsonUtility.FromJson<PlayerData>(loadPlayerData);

        if (saveNumber == "1")
        {
            if (File.Exists(Application.persistentDataPath + "/PlayerData" + 1 + ".json"))
            {
                SetValue1();
            }
            
        }
        else if (saveNumber == "2")
        {
            if (File.Exists(Application.persistentDataPath + "/PlayerData" + 2 + ".json"))
            {
                SetValue2();
            }
        }
        else if (saveNumber == "3")
        {
            if (File.Exists(Application.persistentDataPath + "/PlayerData" + 3 + ".json"))
            {
                SetValue3();
            }
        }
        
    }


    public void LoadGame(int difficulty)
    {
        Cursor.visible = false;
        StartCoroutine(LoadSceneAsync(difficulty));
    }

    //public void SetValue()
    //{
    //    GamePercent1.text = math.round(SaveData.instance.playerData.LevelIndex * 16.66).ToString() + "%";


    //    if (SaveData.instance.playerData.Difficulty == 0)
    //    {
    //        Difficulty1.text = "Easy";
    //    }
    //    else if (SaveData.instance.playerData.Difficulty == 1)
    //    {
    //        Difficulty1.text = "Normal";
    //    }
    //    else if (SaveData.instance.playerData.Difficulty == 2)
    //    {
    //        Difficulty1.text = "Hard";
    //    }


    //    TimeSpan time = TimeSpan.FromSeconds(SaveData.instance.playerData.PlayTime);
    //    GameTime1.text = time.ToString(@"hh\:mm\:ss");
    //    Sprite loadImage = LoadSprite(SaveData.instance.imageSaveFilePath);
    //    Image1.sprite = loadImage;

    //    GameManager.instance.lastLevelIndex = SaveData.instance.playerData.LevelIndex;
    //    GameManager.instance.difficulty = SaveData.instance.playerData.Difficulty;
    //}

    public void SetValue1()
    {
        GamePercent1.text = math.round(SaveData.instance.playerData.LevelIndex * 16.66).ToString() + "%";


        if (SaveData.instance.playerData.Difficulty == 0)
        {
            Difficulty1.text = "Easy";
        }
        else if (SaveData.instance.playerData.Difficulty == 1)
        {
            Difficulty1.text = "Normal";
        }
        else if (SaveData.instance.playerData.Difficulty == 2)
        {
            Difficulty1.text = "Hard";
        }
        

        TimeSpan time = TimeSpan.FromSeconds(SaveData.instance.playerData.PlayTime);
        GameTime1.text = time.ToString(@"hh\:mm\:ss");
        Sprite loadImage = LoadSprite(SaveData.instance.imageSaveFilePath);
        Image1.sprite = loadImage;

        GameManager.instance.lastLevelIndex = SaveData.instance.playerData.LevelIndex;
        GameManager.instance.difficulty = SaveData.instance.playerData.Difficulty;
    }

    public void SetValue2()
    {
        GamePercent2.text = math.round(SaveData.instance.playerData.LevelIndex * 16.66).ToString() + "%";


        if (SaveData.instance.playerData.Difficulty == 0)
        {
            Difficulty2.text = "Easy";
        }
        else if (SaveData.instance.playerData.Difficulty == 1)
        {
            Difficulty2.text = "Normal";
        }
        else if (SaveData.instance.playerData.Difficulty == 2)
        {
            Difficulty2.text = "Hard";
        }


        TimeSpan time = TimeSpan.FromSeconds(SaveData.instance.playerData.PlayTime);
        GameTime2.text = time.ToString(@"hh\:mm\:ss");
        Sprite loadImage = LoadSprite(SaveData.instance.imageSaveFilePath);
        Image2.sprite = loadImage;

        GameManager.instance.lastLevelIndex = SaveData.instance.playerData.LevelIndex;
        GameManager.instance.difficulty = SaveData.instance.playerData.Difficulty;
    }

    public void SetValue3()
    {
        GamePercent3.text = math.round(SaveData.instance.playerData.LevelIndex * 16.66).ToString() + "%";


        if (SaveData.instance.playerData.Difficulty == 0)
        {
            Difficulty3.text = "Easy";
        }
        else if (SaveData.instance.playerData.Difficulty == 1)
        {
            Difficulty3.text = "Normal";
        }
        else if (SaveData.instance.playerData.Difficulty == 2)
        {
            Difficulty3.text = "Hard";
        }


        TimeSpan time = TimeSpan.FromSeconds(SaveData.instance.playerData.PlayTime);
        GameTime3.text = time.ToString(@"hh\:mm\:ss");
        Sprite loadImage = LoadSprite(SaveData.instance.imageSaveFilePath);
        Image3.sprite = loadImage;

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
        }
        catch 
        {
            Debug.Log("There is no save files to load!");
            SaveData.instance.playerData.LevelIndex = 1;
            SaveData.instance.playerData.Difficulty = difficulty;
        }

        AsyncOperation operation = SceneManager.LoadSceneAsync(SaveData.instance.playerData.LevelIndex);

        GameManager.instance.inGame = true;

        LoadingScreen.SetActive(true);

        while (!operation.isDone)
        {           
            yield return null;
        }
    }
}
