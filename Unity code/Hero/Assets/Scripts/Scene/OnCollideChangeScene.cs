using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnCollideChangeScene : MonoBehaviour
{
    public string SceneName;
    public Vector2 otherSide;

    public Animator transition;

    public float transitionTime = 3f;

    public GameObject hero;
    public bool IsDying;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsDying == true && collision.tag == "Player")
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            hero.GetComponent<Health>().TakeDamage(10);

        }
        GameManager.instance.SpawnPoint = otherSide;
        StartCoroutine(LoadLevel());
    }


    IEnumerator LoadLevel()
    {
        transition.Play("In");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(SceneName);
    }
}
