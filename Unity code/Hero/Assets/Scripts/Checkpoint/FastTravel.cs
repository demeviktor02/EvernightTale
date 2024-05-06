using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FastTravel : MonoBehaviour
{
    private GameManager gm;
    public Vector2 position;
    public string SceneName;
    public bool IsUnilocked;
    public GameObject FastTravelLockedButton;


    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (IsUnilocked == true)
        {
            FastTravelLockedButton.SetActive(false);
        }
    }

    public void FastTravelToCheckpoint()
    {
        SceneManager.LoadScene(SceneName);
    }
}
