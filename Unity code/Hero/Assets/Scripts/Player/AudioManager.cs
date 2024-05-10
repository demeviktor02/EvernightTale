using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public SoundLists[] soundLists;

    public string CurrentMusicPlay;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }       

       
    }


    public void PlayAudio(string listName, string audioName)
    {
        SoundLists l = Array.Find(soundLists, sound => sound.listName == listName);
        Sound s = Array.Find(l.list, sound => sound.name == audioName);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }

        if (l.isMusic == true)
        {
            l.source.clip = s.clip;
            l.source.Play();
            CurrentMusicPlay = s.name;
        }
        else if (l.isLooping == true && l.isMusic != true)
        {
            l.source.clip = s.clip;
            l.source.Play();
        }
        else
        {
            l.source.PlayOneShot(s.clip);
        }

    }


    public void StopAudio(string listName)//string audioName
    {
        SoundLists l = Array.Find(soundLists, sound => sound.listName == listName);
        //Sound s = Array.Find(l.list, sound => sound.name == audioName);

        //if (s == null)
        //{
        //    Debug.LogWarning("Sound: " + name + " not found");
        //    return;
        //}

        
          l.source.Stop();
        

    }


}

[System.Serializable]
public class SoundLists
{
    public string listName;

    public Sound[] list;

    public AudioSource source;

    public bool isMusic;

    public bool isLooping;

}
