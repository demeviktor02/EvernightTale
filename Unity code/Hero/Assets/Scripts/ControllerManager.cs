using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControllerManager : MonoBehaviour
{
    public static ControllerManager instance;

    public bool controllerConnected;
    public bool once1;
    public bool once2;
    //public GameObject mainMenuFirst;
    public GameObject lastSelectedgameObject;
    public CharacterName characterName;

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

    void Update()
    {
        string[] names = Input.GetJoystickNames();

        if (names.Length == 0)
        {
            controllerConnected = false;
        }
        else if (names.Length != 0)
        {
            if (names[0] == "")
            {
                controllerConnected = false;
            }
            else if (names[0] != "")
            {
                controllerConnected = true;

            }
        }


        if (controllerConnected && !once1)
        {
            Cursor.visible = false;

            Debug.Log(EventSystem.current.currentSelectedGameObject);
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                once1 = true;
                once2 = false;
            }
            
        }
        else if (!controllerConnected && GameManager.instance.inGame == false)
        {
            Cursor.visible = true;
            if (!characterName.isActive)
            {
                EventSystem.current.SetSelectedGameObject(null);
            }
            

            once2 = true;
            once1 = false;
        }


        if (GameManager.instance.inGame == true && PauseMenu.instance.GameIsPaused == false)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }
    }

    public void SelectGameObject(GameObject gameObject)
    {
        

        lastSelectedgameObject = gameObject;
        if (controllerConnected == true)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
        else if (controllerConnected == false)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
        
    }


    public void NoSelectGameObject()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
}