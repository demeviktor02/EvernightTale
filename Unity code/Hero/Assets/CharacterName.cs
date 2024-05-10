using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterName : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text errorText;
    public LocalManager localManager;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            if (inputField.text == "")
            {
                errorText.gameObject.SetActive(true);
            }
            else
            {
                localManager.LoadGame(0);
            }
        } 
    }
}
