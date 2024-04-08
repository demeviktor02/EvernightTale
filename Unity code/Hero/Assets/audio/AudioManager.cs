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
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null )
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        //s.source.volume = UnityEngine.Random.Range(1 - s.volumeVariance, s.volume);//s.volume;
        //s.source.pitch = UnityEngine.Random.Range(1 - s.pitchVariance, s.pitch); //s.pitch;
        s.source.Play();
        GameManager.instance.currentMusicName = s.name;
    }

    public void Stop()
    {
        foreach (Sound s in sounds)
        {

            s.source.Stop();
        }

    }

    public void StopSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        s.source.Stop();
    }

}
