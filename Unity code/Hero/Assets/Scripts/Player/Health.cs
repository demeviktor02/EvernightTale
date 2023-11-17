using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private GameManager gm;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        if (gm.switchedScene == true)
        {
            gm.switchedScene = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position = gm.switchedScenePosition;
        }
        else
        {
            //if (gm.lastCheckPointSceneName != SceneManager.GetActiveScene().name)
            //{
            //    SceneManager.LoadScene(gm.lastCheckPointSceneName);
            //}
            //transform.position = gm.lastCheckPointPos;
        }



    }

    void Update()
    {



        if (health > numOfHearts)
        {
            health = numOfHearts;
        }

        if (health <= 0 || Input.GetKeyDown(KeyCode.P))
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

    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            Die();
        }
    }
}
