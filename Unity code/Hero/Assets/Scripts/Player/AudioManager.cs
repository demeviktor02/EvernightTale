using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Threading;
using System.Collections;

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
          l.source.Stop();
        

    }

    public IEnumerator Fade(bool fadeIn, string listName, float duration, float targetVolume)
    {
        SoundLists l = Array.Find(soundLists, sound => sound.listName == listName);

        //if (!fadeIn)
        //{
        //    double lenghtOfSource = (double)l.source.clip.samples / l.source.clip.frequency;
        //    yield return new WaitForSecondsRealtime((float)(lenghtOfSource - duration));
        //}

        float time = 0f;
        float startVol = l.source.volume;

        while (time < duration)
        {
            string fadeSituation = fadeIn ? "fadeIn" : "fadeOut";
            Debug.Log(fadeSituation);
            time += Time.deltaTime;
            l.source.volume = Mathf.Lerp(startVol, targetVolume, time / duration);
            yield return null;
        }


        yield break;

    }

    public void TurnDownSound(string listName, float turnDownSpeed)
    {
        SoundLists l = Array.Find(soundLists, sound => sound.listName == listName);
        do
        {
            l.source.volume -= Time.deltaTime * turnDownSpeed;
        } while (l.source.volume == 0);

        l.source.Stop();
    }

    public void TurnDownThenUpSound(string listName, string audioName, float turnDownSpeed, float turnUpSpeed)
    {
        SoundLists l = Array.Find(soundLists, sound => sound.listName == listName);
        do
        {
            l.source.volume -= Time.deltaTime * turnDownSpeed;
        } while (l.source.volume == 0);

        l.source.Stop();

        l.source.volume = 0;
        PlayAudio(listName, audioName);
        do
        {
            l.source.volume += Time.deltaTime * turnUpSpeed;
        } while (l.source.volume == 1);

    }

    public void Mute(string listName)
    {
        SoundLists l = Array.Find(soundLists, sound => sound.listName == listName);
        l.source.volume = 0;
    }

    public void UnMute(string listName)
    {
        SoundLists l = Array.Find(soundLists, sound => sound.listName == listName);
        l.source.volume = 1;
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
