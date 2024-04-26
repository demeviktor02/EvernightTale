using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour
{
    public GameObject mainMenuFirst;
    public GameObject settingMenuMenuFirst;

    public void PlayHoverSound()
    {
        AudioManager.instance.PlayAudio("Menu","Hover");
    }

    public void PlayChooseSound()
    {
        AudioManager.instance.PlayAudio("Menu", "Choose");
    }

    public void PlayChangeSound()
    {
        AudioManager.instance.PlayAudio("Menu", "Change");
        //EventSystem.current.SetSelectedGameObject(settingMenuMenuFirst);
    }

    public void PlayChangeBackSound()
    {
        AudioManager.instance.PlayAudio("Menu", "Change");
        //EventSystem.current.SetSelectedGameObject(mainMenuFirst);
    }
}
