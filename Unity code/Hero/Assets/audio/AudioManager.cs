using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.audioSource;
        }

       
    }

    void Start()
    {
       // Play("Theme");
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null )
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        s.source.Play();

        GameManager.instance.currentMusicName = s.name;
    }

    public void Stop() //string sound
    {
        foreach (Sound s in sounds)
        {
            //s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
            //s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
            s.source.Stop();
        }

        //Sound s = Array.Find(sounds, item => item.name == sound);
        //if (s == null)
        //{
        //    Debug.LogWarning("Sound: " + name + " not found!");
        //    return;
        //}

        //s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        //s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

        //s.source.Stop();
    }

    //just call it like this (AudioManagerReferenceGoeshere).StopPlaying("sound string name");
}
