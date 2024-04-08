using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class EnvironmentAudioManager : MonoBehaviour
{
    public List<EnvironmentSounds> environmentSounds;
    public float timer;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < environmentSounds.Count; i++)
        {
            if (environmentSounds[i].Islooping)
            {
                AudioManager.instance.Play(environmentSounds[i].name);
                environmentSounds[i].time = -1;
            }
            else
            {
                environmentSounds[i].time = Random.Range(0, 5000);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer++;

        foreach (EnvironmentSounds sounds in environmentSounds)
        {
            if (sounds.time == timer)
            {
                AudioManager.instance.Play(sounds.name);
            }
        }

        if (timer >= 5000)
        {
            foreach (EnvironmentSounds sounds in environmentSounds)
            {
                if (sounds.Islooping == false)
                {
                    sounds.time = Random.Range(0, 5000);
                }
            }
            timer = 0;
        }
    }
}

[System.Serializable]
public class EnvironmentSounds
{
    public string name;
    public bool Islooping;
    public float time;
}

