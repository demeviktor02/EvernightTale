using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public Animator animator;
    public bool Iskeyboard;

    public void SwitchControls()
    {
        if (Iskeyboard == true)
        {
            animator.Play("ControllerOpen");
            Iskeyboard = !Iskeyboard;
        }
        else
        {
            animator.Play("KeyboardOpen");
            Iskeyboard = !Iskeyboard;
        }
    }

    public void Keyboard()
    {
        Iskeyboard = true;
    }
}
