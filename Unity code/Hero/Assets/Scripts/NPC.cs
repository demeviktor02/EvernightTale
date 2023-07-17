using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject dialogePanel;
    public TMPro.TMP_Text dialogeText;
    public string[] dialoge;
    private int index;

    public GameObject contButton;
    public float wordSpeed;
    public bool playerIsClose;
    public bool inDialoge = false;
    public bool dialogeEnd = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inDialoge)
        {
            NextLine();
        }

        if (Input.GetKeyDown(KeyCode.E) && playerIsClose && inDialoge == false)
        {
            inDialoge = true;

            if (dialogePanel.activeInHierarchy)
            {
                zeroText();
                
            }
            else
            {
                dialogePanel.SetActive(true);
                StartCoroutine(Typing());
            }
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
        dialogePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialoge[index].ToCharArray())
        {
            dialogeText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
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
            zeroText();
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inDialoge = false;
            playerIsClose = false;
            zeroText();
        }
    }
}
