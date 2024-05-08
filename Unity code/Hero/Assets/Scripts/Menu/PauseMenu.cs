using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;


    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
    //public Animator playerAnimator;

    void OnLoadCallback(Scene scene, LoadSceneMode sceneMode)
    {
        Debug.Log("Pause");
       // playerAnimator = GameObject.FindWithTag("Player").GetComponent<Animator>();
    }

    private void Start()
    {
        SceneManager.sceneLoaded += this.OnLoadCallback;
    }

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
    


        if (Input.GetKeyDown(KeyCode.Escape))
        {
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
        AudioManager.instance.PlayAudio("Music", "Game");
        Cursor.visible = false;
        optionsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
    }

    public void Pause()
    {
        GameIsPaused = true;
        AudioManager.instance.PlayAudio("Music", "Pause");
        Cursor.visible = true;
        StartCoroutine(IPause());       
    }

    public IEnumerator IPause()
    {
        if (GameManager.instance.inGame == true)
        {
            ScreenCapture.CaptureScreenshot(SaveData.instance.imageSaveFilePath);
            yield return new WaitForSeconds(0.1f);
            //playerAnimator.Play("In");
            pauseMenuUI.SetActive(true);
            //playerAnimator.Play("Out");
            Time.timeScale = 0f;
            GameIsPaused = true;
        }

        yield return null;
    }

    public void LoadMenu()
    {
        GameIsPaused = false;
        GameManager.instance.inGame = false;
        GameManager.instance.lastLevelIndex = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1f;
        GameManager.instance.sessionTime = 0;
        SceneManager.LoadScene("Menu");
        
    }

    public void QuitGame()
    {
        GameManager.instance.sessionTime = 0;
        Application.Quit();
    }

}
