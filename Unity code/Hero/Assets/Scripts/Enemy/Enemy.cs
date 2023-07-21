using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;

    
    
    public int maxHealth = 100;
    public int currentHealth;

    public Color basicColor;

    // Start is called before the first frame update
    void Start()
    {
        basicColor = GetComponent<SpriteRenderer>().color;
        currentHealth = maxHealth;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {

        Arrow arrow = hitInfo.GetComponent<Arrow>();
        Destroy(arrow);

    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;

        StartCoroutine(BlinkRed());
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");

        animator.SetBool("IsDead", true);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

        
    }

    public virtual IEnumerator BlinkRed()
    {
        GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(0.2f);

        GetComponent<SpriteRenderer>().color = basicColor;
    }

}
