using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollideSound : MonoBehaviour
{
    public bool start = true;
    public string soundList;
    public string sound;
    public bool music;

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.tag == "Player")
        {
            if (start == true && music == false)
            {
                AudioManager.instance.PlayAudio(soundList, sound);
                start = false;
            }
            else if (start == false && music == false)
            {
                AudioManager.instance.StopAudio(soundList);
                start = true;
            }

            if (music == true && AudioManager.instance.CurrentMusicPlay != sound)
            {
                AudioManager.instance.PlayAudio(soundList, sound); ;
            }
            
        }

    }
}
