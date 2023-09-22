using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnCollideChangeScene : MonoBehaviour
{
    public string SceneName;
    public GameManager gm;
    public Vector2 otherSide;
    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gm.switchedScene = true;
        gm.switchedScenePosition = otherSide;
        SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
    }
}
