using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator;

    public Transform attackPoint;
    public LayerMask enemyLayers;
    public LayerMask treeLayers;

    public float attackRange = 0.5f;
    public int attackDamage = 40;

    public float startAttackRate = 2f;
    public float timeBtwAttack = 0f;

    public bool isAttack = false;
    public bool doubleAttack = false;

    // Update is called once per frame
    void Update()
    {

        if (timeBtwAttack <= 0 && gameObject.GetComponent<PlayerMovement2>().isWalking == false
            && gameObject.GetComponent<PlayerMovement2>().isJumping == false)
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

        if (timeBtwAttack <= 2.5f && timeBtwAttack > 1f && doubleAttack == false && gameObject.GetComponent<PlayerMovement2>().isJumping == false)
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
        Collider2D[] hitTree = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, treeLayers);

        if (hitEnemies.Length == 0)
        {
            Debug.Log(Random.Range(1, 4));
            AudioManager.instance.PlayAudio("Player","PlayerMiss" + Random.Range(1,4));            
        }
        else
        {
            foreach (Collider2D enemy in hitEnemies)
            {

                AudioManager.instance.PlayAudio("Player","PlayerHit1");
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
        }

        if (hitTree.Length == 0)
        {
            Debug.Log(Random.Range(1, 4));
            AudioManager.instance.PlayAudio("Player", "PlayerMiss" + Random.Range(1, 4));
        }
        else
        {
            foreach (Collider2D tree in hitTree)
            {

                AudioManager.instance.PlayAudio("Player", "PlayerHit1");
                tree.GetComponent<Animator>().Play("Falling");
                tree.GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("Ground");
            }
        }

    }

    void DoubleAttack()
    {
        animator.SetBool("DoubleAttack",true);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        if (hitEnemies.Length == 0)
        {
            AudioManager.instance.PlayAudio("Player","PlayerMiss" + Random.Range(1,4));
        }
        else
        {
            foreach (Collider2D enemy in hitEnemies)
            {
                AudioManager.instance.PlayAudio("Player","PlayerHit2");
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
        }       
    }

    public void PlayerAttack()
    {
        isAttack = true;
    }

    public void PlayerNotAttack()
    {
        isAttack = false;
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
