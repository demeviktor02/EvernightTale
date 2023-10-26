using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public string lastCheckPointSceneName;
    public Vector2 lastCheckPointPos;
    public Vector2 switchedScenePosition;

    public bool switchedScene = false;
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

    public void FindSaveManager()
    {
        GameObject.FindWithTag("SaveManager").GetComponent<SaveData>().SaveGame();
    }
}
