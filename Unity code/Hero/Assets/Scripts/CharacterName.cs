using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using UnityEngine.UI;

public class CharacterName : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text errorText;
    public LocalManager localManager;
    public Animator animator;
    public bool once;
    public bool ended;
    public bool isActive;
    public GameObject particles;
    public GameObject enter;
    public GameObject aButton;
    public ButtonSound buttonSound;
    public NPCMenu nPCMenu;

    // Update is called once per frame
    void Update()
    {
        if (inputField.text != "" && once == false)
        {
            animator.Play("EnterShow");
            once = true;
        }

        if (Input.GetButtonDown("Submit") && inputField.gameObject.active == true)
        {
            animator.Play("EnterHide");
            SaveData.instance.playerData.HeroName = inputField.text;
            (nPCMenu.dialoge[2]["variable"] as StringVariable).Value = SaveData.instance.playerData.HeroName;
            ended = true;
            isActive = false;
            nPCMenu.InputField.SetActive(false);
            nPCMenu.InputFieldText.SetActive(false);
            
            nPCMenu.AppearCanvas.SetActive(true);
            nPCMenu.DialogText.SetActive(true);
            nPCMenu.NextLine();
        } 
    }
}
