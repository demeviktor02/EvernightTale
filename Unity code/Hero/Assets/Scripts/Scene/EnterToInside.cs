using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterToInside : MonoBehaviour
{
    public bool playerIsClose;
    public GameObject PressE;
    public string SceneName;

    public GameManager gm;
    public Vector2 otherDoor;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    void Update()
    {
        if (playerIsClose)
        {
            PressE.SetActive(true);
        }
        else
        {
            PressE.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            gm.switchedScene = true;
            gm.switchedScenePosition = otherDoor;
            SceneManager.LoadScene(SceneName);
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
            playerIsClose = false;
        }
    }
}
