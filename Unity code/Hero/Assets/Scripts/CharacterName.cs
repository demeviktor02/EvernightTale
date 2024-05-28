using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterName : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text errorText;
    public LocalManager localManager;
    public Animator animator;
    public bool once;
    public GameObject particles;
    public ButtonSound buttonSound;

    // Update is called once per frame
    void Update()
    {
        if (inputField.text != "" && once == false)
        {
            animator.Play("EnterShow");
            once = true;
        }

        if (Input.GetButtonDown("Submit"))
        {
            particles.SetActive(false);
            buttonSound.StopCampfireSound();
            localManager.LoadGame(localManager.gameModeChanger);

            //if (inputField.text == "")
            //{
            //    errorText.gameObject.SetActive(true);
            //}
            //else
            //{
                    //particles.SetActive(false);
                    //localManager.LoadGame(0);
            //}
        } 
    }
}
