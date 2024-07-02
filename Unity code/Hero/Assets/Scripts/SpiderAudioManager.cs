using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAudioManager : MonoBehaviour
{
    public void PlaySpiderMusic()
    {
        
        
        AudioManager.instance.Mute("Music");
        AudioManager.instance.PlayAudio("Music", "SpiderEscape");
        //AudioManager.instance.TurnUpSound("Music", 2f);
        StartCoroutine(AudioManager.instance.Fade(true, "Music", 2f, 1f));
        
    }

    public void PlayWakeUp()
    {
        
        AudioManager.instance.PlayAudio("Spider", "WakeUp");
    }

    public void PlayBreath()
    {
        AudioManager.instance.PlayAudio("Trigger", "SpiderBreath");
    }

    public void PlayMove()
    {
        AudioManager.instance.PlayAudio("SpiderMove", "Move");
    }
}
