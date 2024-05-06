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

    public Animator transition;
    public float transitionTime = 3f;

    public ParticleSystem dieParticle;


    public Vector2 respawnPoint;

    public Animator CanvasAnimator;

    void Start()
    {
        transform.position = GameManager.instance.SpawnPoint;

        transition.Play("Out");
 

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

    }

    void Update()
    {
        if (health == 4)
        {
            CanvasAnimator.Play("Inda1");
        }
        if (health == 3)
        {
            CanvasAnimator.Play("Inda2");
        }
        if (health == 2)
        {
            CanvasAnimator.Play("Inda3");
        }
        if (health == 1)
        {
            //CanvasAnimator.Play("Inda4");
            CanvasAnimator.Play("LastHealth");
        }


        if (health > numOfHearts)
        {
            health = numOfHearts;
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

        GameManager.instance.SpawnPoint = respawnPoint;
        
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        transition.Play("In");

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
