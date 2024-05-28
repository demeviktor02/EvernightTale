using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsActive : MonoBehaviour
{
    public GameObject go;
    public bool ControllerItem;

    // Update is called once per frame
    void Update()
    {
        if (ControllerManager.instance.controllerConnected && ControllerItem || !ControllerManager.instance.controllerConnected && !ControllerItem)
        {
            go.SetActive(true);
        }
        else
        {
            go.SetActive(false);
        }
    }
}
