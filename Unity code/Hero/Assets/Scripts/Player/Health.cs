using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static System.TimeZoneInfo;

public class Health : MonoBehaviour
{
    public int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public Animator animator;
    public ParticleSystem dieParticle;


    public Vector2 otherSide;
    public float transitionTime = 3f;

    void Start()
    {
        GameManager.instance.transition.Play("LevelLoaderEnd");

        if (GameManager.instance.difficulty == 0)
        {
            numOfHearts = 5;
        }
        else if (GameManager.instance.difficulty == 1)
        {
            numOfHearts = 4;
        }
        else if (GameManager.instance.difficulty == 2)
        {
            numOfHearts = 3;
        }

        if (GameManager.instance.switchedScene == true)
        {
            GameManager.instance.switchedScene = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position = GameManager.instance.switchedScenePosition;
        }



    }

    void Update()
    {



        if (health > numOfHearts)
        {
            health = numOfHearts;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Die();
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }


            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void Die()
    {
        health = 0;
        gameObject.GetComponent<PlayerMovement2>().enabled = false;

        float randomNumer = Random.Range(1, 2);
        if (randomNumer == 0)
        {
            Debug.Log("ASD");
            animator.SetTrigger("IsDying");
        }
        else
        {
            Debug.Log("BSD");
            animator.SetTrigger("IsDying2");

        }
        
        dieParticle.Play();

        GameManager.instance.switchedScene = true;
        GameManager.instance.switchedScenePosition = otherSide;
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        GameManager.instance.transition.Play("LevelLoaderStart");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            Die();
        }
    }
}
