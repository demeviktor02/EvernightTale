using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuFirst;
    public bool once;
    public float timer;
    public Animator logoAnimator;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 20f)
        {
            logoAnimator.Play("Disappear");
            timer = 0f;
        }


        if (Gamepad.current != null && once == false)
        {
            EventSystem.current.SetSelectedGameObject(mainMenuFirst);
            Cursor.visible = false;
            once = true;
        }
        else if (Gamepad.current == null && once == true)
        {
            EventSystem.current.SetSelectedGameObject(null);
            Cursor.visible = true;
            once = false;
        }
    }

    //private void Start()
    //{
    //    EventSystem.current.SetSelectedGameObject(mainMenuFirst);
    //}

    public void PlayGame()
    {
        GameManager.instance.inGame = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadGame()
    {
        GameManager.instance.inGame = true;
        StartCoroutine(Load());
    }

    public IEnumerator Load()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(GameManager.instance.lastLevelIndex);
    }


    public void PlayLevel(int index)
    {
        GameManager.instance.inGame = true;
        StartCoroutine(Play(index));
    }

    public IEnumerator Play(int index)
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(index);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
