using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    public int currentHealth;
    public ParticleSystem deathParticles;

    void Start()
    {
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

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        SaveData.instance.playerData.SlayedEnemies++;
        Debug.Log("Enemy died!");

        animator.SetBool("IsDead", true);
        deathParticles.Play();

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

    }

}
