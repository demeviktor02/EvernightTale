using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject dialogePanel;
    public TMPro.TMP_Text dialogeText;
    public TMPro.TMP_Text nameText;
    public LocalizedString[] dialoge;
    private int index;

    public bool currentDialogEnd = false;

    public GameObject contButton;
    public GameObject EndButton;
    public float wordSpeed;
    public bool playerIsClose;
    public bool inDialoge = false;
    public bool dialogeEnd = false;

    public GameObject PressE;

    public Animator animator;

    public GameObject NPCCam;

    public GameObject player;

    public LocalizedString[] localizedString;

    public float waitTime;

    public Video2 video2;

    public bool IsCutscene;

    // Update is called once per frame
    private void Start()
    {
        (localizedString[1]["heroname"] as StringVariable).Value = SaveData.instance.playerData.HeroName;
        if (IsCutscene)
        {
            (localizedString[3]["heroname"] as StringVariable).Value = SaveData.instance.playerData.HeroName;
        }
        
    }

    void Update()
    {


        if (Input.GetButtonDown("Talk") && inDialoge && currentDialogEnd == true)
        {
            currentDialogEnd = false;
            NextLine();
        }

        if (Input.GetButtonDown("Talk") && playerIsClose && inDialoge == false)
        {
            player.GetComponent<Animator>().Play("Idle");
            nameText.text = localizedString[0].GetLocalizedString();
            player.GetComponent<PlayerMovement2>().enabled = false;
            NPCCam.SetActive(true);
            inDialoge = true;
            animator.Play("TextAppear");
            zeroText();
            StartCoroutine(StartTalking());
        }

        if (dialogeText.text == dialoge[index].GetLocalizedString())
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
        yield return new WaitForSeconds(waitTime);

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

    public void NextLine()
    {
        contButton.SetActive(false);

        if (index < dialoge.Length - 1)
        {
            if (IsCutscene == false && index == 1)
            {
                AudioManager.instance.PlayAudio("Village", "RatGirl");
            }

            nameText.text = localizedString[index + 1].GetLocalizedString().ToUpper();
            index++;
            dialogeText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            animator.Play("TalkingEnd");
            
            player.GetComponent<PlayerMovement2>().enabled = true;
            zeroText();


            if (IsCutscene == true)
            {
                video2.GetComponent<Animator>().Play("Start");
            }
            else
            {
                NPCCam.SetActive(false);
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.Play("Appear");
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.Play("Disappear");
            playerIsClose = false;
            inDialoge = false;

        }
    }
}
