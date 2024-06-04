using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

public class RespawnMagager : MonoBehaviour
{
    public PauseMenu pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        
        //GameManager.instance.SpawnPoint = spawnPoint;
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<PauseMenu>();
        pauseMenu.transitionAnimator = GameObject.FindWithTag("Transition").GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
