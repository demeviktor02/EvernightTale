using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    private GameManager gm;
    public GameObject PressE;
    public bool playerIsClose;
    public GameObject FastTravelMenu;
    public int CheckpointNumber;


    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        FastTravelMenu = GameObject.FindGameObjectWithTag("FastTravelMenu").gameObject.transform.GetChild(0).gameObject;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            FastTravelMenu.SetActive(true);
        }

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gm.lastCheckPointSceneName = SceneManager.GetActiveScene().name;
            gm.lastCheckPointPos = transform.position;
            GetComponent<SpriteRenderer>().color = Color.green;
            PressE.SetActive(true);
            playerIsClose = true;
            FastTravelMenu.gameObject.transform.GetChild(CheckpointNumber).GetComponent<FastTravel>().IsUnilocked = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PressE.SetActive(false);
            playerIsClose = false;
        }
    }
}
