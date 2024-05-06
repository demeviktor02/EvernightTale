using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int lastLevelIndex = 0;
    public Vector2 SpawnPoint;
    public bool inGame;

    //public Joystick joystick;
    public Button jumpButton;
    public Button pauseButton;
    public Button changeWeaponButton;

    public float sessionTime;

    public int difficulty;

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

    private void Update()
    {

        if (inGame)
        {
            sessionTime += Time.deltaTime;
        }

    }

}