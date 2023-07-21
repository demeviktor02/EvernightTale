using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : Enemy
{
	public GameObject go;
	//public int health = 500;

	public GameObject deathEffect;

	public bool isInvulnerable = false;

 //   private void Start()
 //   {
	//	basicColor = GetComponent<SpriteRenderer>().color;
	//}

    public override void TakeDamage(int damage)
	{
		if (isInvulnerable)
			return;

		currentHealth -= damage;
		//StartCoroutine(BlinkRed());
		animator.SetTrigger("Hurt");

		if (currentHealth <= 200)
		{
			GetComponent<Animator>().SetBool("IsEnraged", true);
		}


		if (currentHealth <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		//Instantiate(deathEffect, transform.position, Quaternion.identity);
		animator.SetBool("IsDead", true);
		GetComponent<Collider2D>().enabled = false;
		this.enabled = false;

		go.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D hitInfo)
	{
		//TakeDamage(20);
		Arrow arrow = hitInfo.GetComponent<Arrow>();
		Destroy(arrow);

	}

	

}