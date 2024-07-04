using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ControllerManager : MonoBehaviour
{
    public static ControllerManager instance;

    public bool controllerConnected;
    public bool once1;
    public GameObject lastSelectedgameObject;
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


        if (Gamepad.current == null)
        {
            controllerConnected = false;
        }
        else if (Gamepad.current != null)
        {
            controllerConnected = true;
        }


        if (controllerConnected && !once1 && GameManager.instance.inGame == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (EventSystem.current.currentSelectedGameObject != null)
            {
                once1 = true;
            }

            EventSystem.current.SetSelectedGameObject(lastSelectedgameObject);
        }
        else if (!controllerConnected && GameManager.instance.inGame == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            once1 = false;

            EventSystem.current.SetSelectedGameObject(null);
        }


        if (GameManager.instance.inGame == true && PauseMenu.instance.GameIsPaused == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (GameManager.instance.inGame == true && PauseMenu.instance.GameIsPaused == true)
        {
            if (controllerConnected && !once1)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                
                if (EventSystem.current.currentSelectedGameObject != null)
                {
                    once1 = true;
                }

                EventSystem.current.SetSelectedGameObject(lastSelectedgameObject);

            }
            else if (!controllerConnected)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                once1 = false;

                EventSystem.current.SetSelectedGameObject(null);
            }
            
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
