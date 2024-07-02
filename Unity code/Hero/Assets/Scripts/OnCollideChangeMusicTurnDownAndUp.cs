using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollideChangeMusicTurnDownAndUp : MonoBehaviour
{
    public string soundList;
    public string startSound;
    public float waitTime;
    public bool once;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && once == false)
        {
            once = true;
            StartCoroutine(StartFadeInAndOut());
        }

    }

    public IEnumerator StartFadeInAndOut()
    {
        StartCoroutine(AudioManager.instance.Fade(false, "Music", 5f, 0f));

        yield return new WaitForSeconds(waitTime);

        AudioManager.instance.PlayAudio(soundList, startSound);
        StartCoroutine(AudioManager.instance.Fade(true, "Music", 5f, 1f));

        yield return null;
    }
}
