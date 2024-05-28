using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Video : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        if ((videoPlayer.frame) > 0 && (videoPlayer.isPlaying == false))
        {
            animator.Play("EndCutscene");
        }
        

        if (Input.GetButtonDown("Cancel"))
        {
            animator.Play("EndCutscene");
        }
    }

    public void PlayVideo()
    {
        videoPlayer.Play();
    }
}
