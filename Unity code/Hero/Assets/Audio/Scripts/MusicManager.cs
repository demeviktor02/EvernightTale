using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public string musicToStart;
    void Start()
    {

        if (AudioManager.instance.CurrentMusicPlay != musicToStart)
        {

            //AudioManager.instance.Stop();

            AudioManager.instance.PlayAudio("Music", musicToStart);
        }      

    }
}
