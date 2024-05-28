using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBack : MonoBehaviour
{
    public Animator animator;
    public string animationName;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            animator.Play(animationName);
        }
    }
}
