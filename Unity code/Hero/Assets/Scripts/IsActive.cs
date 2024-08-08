using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsActive : MonoBehaviour
{
    public GameObject KeyboardItem;
    public GameObject ControllerItem;
    public GameObject Androiditem;
    //public bool ControllerItem;
    //public bool MobileItem;

    // Update is called once per frame
    void Update()
    {
        if (ControllerManager.instance.controllerConnected)
        {
            KeyboardItem.SetActive(false);
            ControllerItem.SetActive(true);
            Androiditem.SetActive(false);
        }
        else if (Application.platform == RuntimePlatform.Android &&
            Application.platform == RuntimePlatform.IPhonePlayer && !ControllerManager.instance.controllerConnected)
        {
            KeyboardItem.SetActive(false);
            ControllerItem.SetActive(false);
            Androiditem.SetActive(true);
        }
        else
        {
            KeyboardItem.SetActive(true);
            ControllerItem.SetActive(false);
            Androiditem.SetActive(false);
        }


            //if (ControllerManager.instance.controllerConnected && ControllerItem || !ControllerManager.instance.controllerConnected && !ControllerItem)
            //{
            //    go.SetActive(true);
            //}
            //else
            //{
            //    go.SetActive(false);
            //}

        
    }
}
