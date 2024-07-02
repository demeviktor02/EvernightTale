using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBack : MonoBehaviour
{
    public Animator animator;
    public LocalManager localManager;
    public bool InGameSelection;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (InGameSelection)
            {
                animator.Play("ChooseDifficultyClose");
            }
            else
            {
                animator.Play("PlayBack");
                localManager.paperHoveredBool = false;
            }

            ControllerManager.instance.NoSelectGameObject();
        }
    }
}
