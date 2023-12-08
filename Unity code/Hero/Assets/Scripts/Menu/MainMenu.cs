using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(GameManager.instance.lastLevelIndex);
    }

    public void ContinueGame()
    {

    }

    public void PlayLevel(int index)
    {
        SceneManager.LoadScene(index);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
