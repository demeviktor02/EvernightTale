using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;


    public bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
    public Animator transitionAnimator;
    public Animator pauseMenuAnimator;
    public GameObject hover;

    public bool pausing;

    void Awake()
    {
        

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }

        instance.pauseMenuUI.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {        

        if (Input.GetButtonDown("Pause") && GameManager.instance.inCutscene == false && GameManager.instance.inGame == true && pausing == false)
        {
            pausing = true;

            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void MobilePause()
    {
        if (GameManager.instance.inCutscene == false && GameManager.instance.inGame == true && pausing == false)
        {
            pausing = true;

            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        GameIsPaused = false;
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        StartCoroutine(IResume());
        
    }

    public IEnumerator IResume()
    {
        pauseMenuAnimator.Play("In");
        yield return new WaitForSecondsRealtime(2f);
        optionsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(false);

        transitionAnimator.Play("Out");
        AudioManager.instance.PlayAudio("Music", "Game");
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1f;
        AudioManager.instance.UnMute("ForestLoop");
        AudioManager.instance.UnMute("Trigger");
        ControllerManager.instance.SelectGameObject(null);
        pausing = false;

        yield return null;
    }

    public void RestartAnim()
    {
        AudioManager.instance.PlayAudio("Menu", "Play");
        pauseMenuAnimator.Play("NewGame");
    }

    public void Restart()
    {
        GameManager.instance.SpawnPoint = new Vector2(-79.11f, 7.68f);
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        SceneManager.LoadScene("FirstLevel");
        AudioManager.instance.StopAudio("Trigger");
        AudioManager.instance.UnMute("ForestLoop");
        AudioManager.instance.UnMute("Trigger");
    }

    public void Pause()
    {
        GameIsPaused = true;
        //if (ControllerManager.instance.controllerConnected == false)
        //{
        //    Cursor.lockState = CursorLockMode.None;
        //    Cursor.visible = true;
        //}
        
        StartCoroutine(IPause());       
    }

    public IEnumerator IPause()
    {
        if (GameManager.instance.inGame == true)
        {
            ScreenCapture.CaptureScreenshot(SaveData.instance.imageSaveFilePath);

            //yield return new WaitForSecondsRealtime(0.1f);

            Time.timeScale = 0f;
            AudioManager.instance.Mute("ForestLoop");
            AudioManager.instance.Mute("Trigger");
            transitionAnimator.Play("In");

            yield return new WaitForSecondsRealtime(3f);        
            
            pauseMenuUI.SetActive(true);
            pauseMenuAnimator.Play("Out");
            AudioManager.instance.PlayAudio("Music", "Pause");
            ControllerManager.instance.SelectGameObject(hover);
        }

        pausing = false;

        yield return null;
    }

    public void LoadMenu()
    {
        GameIsPaused = false;
        GameManager.instance.inGame = false;
        GameManager.instance.lastLevelIndex = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1f;
        GameManager.instance.sessionTime = 0;
        AudioManager.instance.UnMute("ForestLoop");
        AudioManager.instance.UnMute("Forest");
        AudioManager.instance.UnMute("Trigger");
        AudioManager.instance.StopAudio("Trigger");
        SceneManager.LoadScene("Menu");
        
    }

    public void QuitGame()
    {
        GameManager.instance.sessionTime = 0;
        Application.Quit();
    }

}
