using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectGameObject : MonoBehaviour
{
    public GameObject story;
    public GameObject paper;
    public GameObject play;

    public void SelectGameObjectStory()
    {
        ControllerManager.instance.SelectGameObject(null);
        ControllerManager.instance.SelectGameObject(story);
    }

    public void SelectGameObjectPaper()
    {
        ControllerManager.instance.SelectGameObject(null);
        ControllerManager.instance.SelectGameObject(paper);
    }

    public void SelectGameObjectPlay()
    {
        ControllerManager.instance.SelectGameObject(null);
        ControllerManager.instance.SelectGameObject(play);
    }
}
