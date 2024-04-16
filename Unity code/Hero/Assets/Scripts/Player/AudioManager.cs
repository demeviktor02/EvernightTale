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


    public void StopAudio(string listName, string audioName)
    {
        SoundLists l = Array.Find(soundLists, sound => sound.listName == listName);
        Sound s = Array.Find(l.list, sound => sound.name == audioName);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }

        
          l.source.Stop();
        

    }


    //public void PlayMusic (string name)
    //{
    //    Sound s = Array.Find(musicSounds, sound => sound.name == name);

    //    if (s == null )
    //    {
    //        Debug.LogWarning("Sound: " + name + " not found");
    //        return;
    //    }
    //    else
    //    {
    //        s.source.clip = s.clip;
    //        s.source.Play();
    //        CurrentMusicPlay = s.name;
    //    }

    //}


    //public void PlaySFX(string name)
    //{
    //    Sound s = Array.Find(sfxSounds, sound => sound.name == name);

    //    if (s == null)
    //    {
    //        Debug.LogWarning("Sound: " + name + " not found");
    //        return;
    //    }
    //    else if (s.loop == true)
    //    {
    //        s.source.clip = s.clip;
    //        s.source.Play();
    //    }
    //    else
    //    {
    //        s.source.PlayOneShot(s.clip);
    //    }

    //}

    //public void PlayEnvironmentSound(string name)
    //{
    //    Sound s = Array.Find(environmentSounds, sound => sound.name == name);

    //    if (s == null)
    //    {
    //        Debug.LogWarning("Sound: " + name + " not found");
    //        return;
    //    }
    //    else
    //    {
    //        s.source.clip = s.clip;
    //        s.source.Play();
    //        CurrentMusicPlay = s.name;
    //    }

    //}

    //public void StopSFX(string name)
    //{
    //    Sound s = Array.Find(sfxSounds, sound => sound.name == name);

    //    if (s == null)
    //    {
    //        Debug.LogWarning("Sound: " + name + " not found");
    //        return;
    //    }
    //    else if (s.loop == true)
    //    {
    //        s.source.clip = s.clip;
    //        s.source.Stop();
    //    }
    //    else
    //    {
    //        s.source.Stop();
    //    }

    //}

    //public void Stop()
    //{
    //    foreach (Sound s in musicSounds)
    //    {

    //        s.source.Stop();
    //    }

    //}

    //public void StopSound(string name)
    //{
    //    Sound s = Array.Find(musicSounds, sound => sound.name == name);
    //    if (s == null)
    //    {
    //        Debug.LogWarning("Sound: " + name + " not found");
    //        return;
    //    }
    //    s.source.Stop();
    //}

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
