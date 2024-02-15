using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public int attackDamage = 40;

    public float startAttackRate = 2f;
    public float timeBtwAttack = 0f;

    public bool doubleAttack = false;

    // Update is called once per frame
    void Update()
    {

        if (timeBtwAttack <= 0)
        {
            doubleAttack = false;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
                timeBtwAttack = startAttackRate;
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }

        if (timeBtwAttack <= 2.5f && timeBtwAttack > 1f && doubleAttack == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                doubleAttack = true;
                DoubleAttack();
                animator.SetTrigger("DoubleAttack");
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }


    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    void DoubleAttack()
    {
        animator.SetBool("DoubleAttack",true);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
