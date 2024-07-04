using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class NPCMenu : MonoBehaviour
{
    public GameObject dialogePanel;
    public TMPro.TMP_Text dialogeText;
    public TMPro.TMP_Text nameText;
    public LocalizedString[] dialoge;
    public int index;

    public bool currentDialogEnd = false;

    public GameObject contButton;
    public GameObject EndButton;
    public float wordSpeed;
    public bool inDialoge = false;
    public bool dialogeEnd = false;
    public bool startBool = false;

    public LocalizedString[] localizedString;
    public LocalManager localManager;

    public GameObject InputField;
    public GameObject InputFieldText;

    public GameObject AppearCanvas;
    public GameObject DialogText;
    public CharacterName characterName;

    public bool hideContinue;

    public TMP_InputField TMPInputField;

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetButton("Talk") && inDialoge && currentDialogEnd == true)
        {
            Debug.Log(dialoge.Length);
            Debug.Log(index);

            currentDialogEnd = false;
            NextLine();
        }

        if (startBool == true && inDialoge == false)
        {
            startBool = false;
            nameText.text = localizedString[0].GetLocalizedString();
            inDialoge = true;
            zeroText();
            StartCoroutine(StartTalking());
        }

        if (dialogeText.text == dialoge[index].GetLocalizedString() && hideContinue == false)
        {
            contButton.SetActive(true);
        }
    }

    public void zeroText()
    {
        dialogeText.text = "";
        index = 0;
    }

    IEnumerator StartTalking()
    {
        StartCoroutine(Typing());

        yield return null;
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialoge[index].GetLocalizedString().ToCharArray())
        {
            dialogeText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
        currentDialogEnd = true;
    }

    public IEnumerator WaitForInputActivation()
    {
        yield return new WaitForSeconds(1f);
        TMPInputField.ActivateInputField();
    }

    public void NextLine()
    {
        contButton.SetActive(false);

        if (index < dialoge.Length - 1)
        {
            if (index == 1 && characterName.ended == false)
            {
                hideContinue = true;


                InputField.SetActive(true);
                InputFieldText.SetActive(true);
                AppearCanvas.SetActive(false);
                DialogText.SetActive(false);
                characterName.isActive = true;
                StartCoroutine(WaitForInputActivation());
            }
            else
            {
                hideContinue = false;

                index++;
                nameText.text = localizedString[index].GetLocalizedString();
                dialogeText.text = "";
                StartCoroutine(Typing());
            }

            
        }
        else
        {
            characterName.particles.SetActive(false);
            AppearCanvas.SetActive(false);
            DialogText.SetActive(false);
            localManager.LoadGame(localManager.gameModeChanger);
            zeroText();
        }
    }

}
