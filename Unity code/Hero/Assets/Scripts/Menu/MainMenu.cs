using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

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

    public void ContinueGame()
    {

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
