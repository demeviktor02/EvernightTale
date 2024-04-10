using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] musicSounds, sfxSounds, environmentSounds;

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

        

        //foreach (Sound s in musicSounds)
        //{
        //    s.source = gameObject.AddComponent<AudioSource>();
        //    s.source.clip = s.clip;

        //    s.source.volume = s.volume;
        //    s.source.pitch = s.pitch;
        //    s.source.loop = s.loop;
        //    s.source.outputAudioMixerGroup = s.audioSource;
        //}

       
    }

    public void PlayMusic (string name)
    {
        Sound s = Array.Find(musicSounds, sound => sound.name == name);

        if (s == null )
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        else
        {
            s.source.clip = s.clip;
            s.source.Play();
            CurrentMusicPlay = s.name;
        }

    }


    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        else if (s.loop == true)
        {
            s.source.clip = s.clip;
            s.source.Play();
        }
        else
        {
            s.source.PlayOneShot(s.clip);
        }

    }

    public void PlayEnvironmentSound(string name)
    {
        Sound s = Array.Find(environmentSounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        else
        {
            s.source.clip = s.clip;
            s.source.Play();
            CurrentMusicPlay = s.name;
        }

    }

    public void StopSFX(string name)
    {
        Sound s = Array.Find(sfxSounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        else if (s.loop == true)
        {
            s.source.clip = s.clip;
            s.source.Stop();
        }
        else
        {
            s.source.Stop();
        }

    }

    public void Stop()
    {
        foreach (Sound s in musicSounds)
        {

            s.source.Stop();
        }

    }

    public void StopSound(string name)
    {
        Sound s = Array.Find(musicSounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        s.source.Stop();
    }

}
