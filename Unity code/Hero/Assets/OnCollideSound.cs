using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollideSound : MonoBehaviour
{
    public bool start = true;
    public string triggerSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.tag == "Player")
        {
            if (start == true)
            {
                AudioManager.instance.PlayAudio("Trigger", triggerSound);
                start = false;
            }
            else if (start == false)
            {
                AudioManager.instance.StopAudio("Trigger", triggerSound);
                start = true;
            }
            
        }

    }
}
