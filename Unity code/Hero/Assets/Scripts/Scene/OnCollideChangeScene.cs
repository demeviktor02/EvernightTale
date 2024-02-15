using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnCollideChangeScene : MonoBehaviour
{
    public string SceneName;
    public GameManager gm;
    public Vector2 otherSide;

    public Animator transition;

    public float transitionTime = 3f;

    public Animator heroAnimator;
    public GameObject hero;
    public bool IsDying;
    private void Start()
    {
        transition = GameManager.instance.transition;
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsDying == true)
        {
            hero.GetComponent<PlayerMovement2>().enabled = false;
            heroAnimator.SetTrigger("IsDying");
        }
        gm.switchedScene = true;
        gm.switchedScenePosition = otherSide;
        StartCoroutine(LoadLevel());
    }


    IEnumerator LoadLevel()
    {
        GameManager.instance.transition.Play("LevelLoaderStart");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(SceneName);
    }
}
