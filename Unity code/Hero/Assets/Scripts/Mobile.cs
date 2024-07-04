using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mobile : MonoBehaviour
{
    public bool once;

    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform != RuntimePlatform.Android || Application.platform != RuntimePlatform.IPhonePlayer)
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (GameManager.instance.inCutscene)
        {

        }
    }

    public void MobilePause()
    {
        PauseMenu.instance.MobilePause();
    }
}
