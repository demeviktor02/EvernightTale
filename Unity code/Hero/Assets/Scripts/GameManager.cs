using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int lastLevelIndex = 0;
    public string lastCheckPointSceneName;
    public Vector2 lastCheckPointPos;
    public Vector2 switchedScenePosition;
    public bool inGame;

    public Joystick joystick;
    public Button jumpButton;
    public Button pauseButton;
    public Button changeWeaponButton;

    public bool switchedScene = false;

    public Animator transition;

    public string currentMusicName;


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

    //public void FindSaveManager()
    //{
    //    GameObject.FindWithTag("SaveManager").GetComponent<SaveData>().SaveGame();
    //}
}
