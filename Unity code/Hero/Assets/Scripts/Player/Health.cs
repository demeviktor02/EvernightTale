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

    public Animator CanvasAnimator;

    public bool IsDying;
    public bool IsDyingFromWater;
    public bool IsDyingFromFalling;

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
        IsDying = true;

        AudioManager.instance.StopAudio("PlayerRun");
        AudioManager.instance.StopAudio("Trigger");
        health = 0;
        gameObject.GetComponent<PlayerMovement2>().enabled = false;

        if (!IsDyingFromFalling)
        {
            animator.SetTrigger("IsDying2");
        }
        
                
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
        if (IsDying == false)
        {
            if (!IsDyingFromWater)
            {
                AudioManager.instance.PlayAudio("Player", "PlayerHurt" + Random.Range(1, 3));
            }
            else if (IsDyingFromWater)
            {
                AudioManager.instance.PlayAudio("Player", "Water");
            }
            

            takeDamage = true;

            healthTimer = 10f;

            health -= damage;

            if (health <= 0)
            {
                Die();
            }
        }       
        
    }

    public void DieSound(int sound)
    {
        if (!IsDyingFromWater && !IsDyingFromFalling)
        {
            if (sound == 0)
            {
                AudioManager.instance.PlayAudio("Player", "PlayerDie1");
            }
            else if (sound == 1)
            {
                AudioManager.instance.PlayAudio("Player", "PlayerDieSword1");
            }
            else if (sound == 2)
            {
                AudioManager.instance.PlayAudio("Player", "PlayerDie2");
            }
            else if (sound == 3)
            {
                AudioManager.instance.PlayAudio("Player", "PlayerDieSword2");
            }
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
