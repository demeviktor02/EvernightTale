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
        AudioManager.instance.PlayAudio("Menu", "Hover" + Random.Range(1, 3));
    }

    public void PlayChooseSound()
    {
        AudioManager.instance.PlayAudio("Menu", "Choose");
    }

    public void PlayChangeSound()
    {
        AudioManager.instance.PlayAudio("Menu", "Change");
    }

    public void PlayChangeBackSound()
    {
        AudioManager.instance.PlayAudio("Menu", "Change");
    }

    public void PlayPlaySound()
    {
        AudioManager.instance.PlayAudio("Menu", "Play");
    }

    public void PlayCampfireSound()
    {
        AudioManager.instance.PlayAudio("Trigger", "Campfire");
    }

    public void StopCampfireSound()
    {
        AudioManager.instance.StopAudio("Trigger");
    }
}
