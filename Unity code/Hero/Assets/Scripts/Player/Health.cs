using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int health;
    public bool takeDamage;
    public float healthTimer = 0f;

    public Animator animator;

    public Animator transition;
    public float transitionTime = -1f;

    public ParticleSystem dieParticle;


    public Vector2 respawnPoint;

    public Animator CanvasAnimator;

    void Start()
    {
        transform.position = GameManager.instance.SpawnPoint;

        transition.Play("Out");
 

        if (GameManager.instance.difficulty == 0)
        {
            
        }
        else if (GameManager.instance.difficulty == 1)
        {
            
        }
        else if (GameManager.instance.difficulty == 2)
        {
            
        }

    }

    void Update()
    {
        if (healthTimer >= -1)
        {
            healthTimer -= Time.deltaTime;
        }

        if (health == 4 && takeDamage == true)
        {
            CanvasAnimator.Play("Inda1");
            takeDamage = false;
        }
        if (health == 3 && takeDamage == true)
        {
            CanvasAnimator.Play("Inda2");
            takeDamage = false;
        }
        if (health == 2 && takeDamage == true)
        {
            CanvasAnimator.Play("Inda3");
            takeDamage = false;
        }
        if (health == 1 && takeDamage == true)
        {
            //CanvasAnimator.Play("Inda4");
            CanvasAnimator.Play("LastHealth");
            takeDamage = false;
        }

        if (health == 1 && healthTimer < 0)
        {
            CanvasAnimator.Play("Inda4Out");
            healthTimer = 10f;
            health = 2;
        }
        if (health == 2 && healthTimer < 0)
        {
            CanvasAnimator.Play("Inda3Out");
            healthTimer = 10f;
            health = 3;
        }
        if (health == 3 && healthTimer < 0)
        {
            CanvasAnimator.Play("Inda2Out");
            healthTimer = 10f;
            health = 4;
        }
        if (health == 4 && healthTimer < 0)
        {
            CanvasAnimator.Play("Inda1Out");
            health = 5;
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
        takeDamage = true;

        healthTimer = 10f;

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
