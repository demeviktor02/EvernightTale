using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public string musicToStart;
    //public List<string> musicToStop;
    void Start()
    {

        if (GameManager.instance.currentMusicName != musicToStart)
        {

            AudioManager.instance.Stop();

            AudioManager.instance.Play(musicToStart);
        }      

        //for (int i = 0; i < musicToStop.Count; i++)
        //{
        //    AudioManager.instance.Stop(musicToStop[i]);
        //}
    }
}
