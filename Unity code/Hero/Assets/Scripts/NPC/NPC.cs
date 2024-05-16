using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject dialogePanel;
    public TMPro.TMP_Text dialogeText;
    public TMPro.TMP_Text nameText;
    public string[] dialoge;
    public string[] Names;
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

    // Update is called once per frame
    void Update()
    {
        //if (playerIsClose && inDialoge == false)
        //{

        //}
        //else
        //{
        //    animator.Play("Disappear");
        //}


        if (Input.GetKeyDown(KeyCode.E) && inDialoge && currentDialogEnd == true)
        {
            currentDialogEnd = false;
            nameText.text = Names[index+1];
            NextLine();
        }

        if (Input.GetKeyDown(KeyCode.E) && playerIsClose && inDialoge == false)
        {
            player.GetComponent<Animator>().Play("Idle");
            nameText.text = Names[0];
            player.GetComponent<PlayerMovement2>().enabled = false;
            NPCCam.SetActive(true);
            inDialoge = true;
            animator.Play("TextAppear");
            zeroText();
            StartCoroutine(Typing());
        }

        if (dialogeText.text == dialoge[index])
        {
            contButton.SetActive(true);
        }
    }

    public void zeroText()
    {
        dialogeText.text = "";
        index = 0;
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialoge[index].ToCharArray())
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
            index++;
            dialogeText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            animator.Play("TalkingEnd");
            NPCCam.SetActive(false);
            player.GetComponent<PlayerMovement2>().enabled = true;
            zeroText();
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
