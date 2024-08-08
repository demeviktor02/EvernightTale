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
    public string heroName;

    public Joystick joystick;
    public Button jumpButton;
    public Button pauseButton;
    public Button talkButton;

    public float sessionTime;

    public int difficulty;

    public bool inCutscene;

    public int current;
    public TMPro.TMP_Text fps;

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
        //Application.targetFrameRate = 60;
        //QualitySettings.vSyncCount = 0;

        current = (int)(1f / Time.unscaledDeltaTime);
        fps.text = current.ToString();

        if (inGame)
        {
            sessionTime += Time.deltaTime;
        }

        if (inGame && Application.platform == RuntimePlatform.Android || inGame && Application.platform == RuntimePlatform.IPhonePlayer)
        {
            joystick.GetComponent<GameObject>().SetActive(true);
            jumpButton.GetComponent<GameObject>().SetActive(true);
            pauseButton.GetComponent<GameObject>().SetActive(true);
            talkButton.GetComponent<GameObject>().SetActive(true);
        }
    }

}
