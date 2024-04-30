using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInda : MonoBehaviour
{
    public GameObject gameObjectSetActive;
    public GameObject gameObjectSetNotActive;
    public GameObject gameObjectSelf;

    public void setActive()
    {
        gameObjectSetActive.SetActive(true);
    }

    public void setNotActive()
    {
        gameObjectSetNotActive.SetActive(false);
    }


    public void setActive2()
    {
        gameObjectSetNotActive.SetActive(true);
    }

    public void setNotActive2()
    {
        gameObjectSetActive.SetActive(false);
    }

    public void setNotSelf()
    {
        gameObject.SetActive(false);
    }
}
