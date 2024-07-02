using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
public class FollowEnemy : MonoBehaviour
{
    public float speed;
    public Transform target;
    public Animator animator;

    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask obstacleMask;
    public LayerMask playerMask;
    public LayerMask ratMask;

    Collider2D obstacleColInfo;
    public Collider2D playerColInfo;
    Collider2D ratColInfo;

    void Update()
    {


        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        

        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        obstacleColInfo = Physics2D.OverlapCircle(pos, attackRange, obstacleMask);
        if (obstacleColInfo != null)
        {
            animator.SetTrigger("Attack");
        }


        playerColInfo = Physics2D.OverlapCircle(pos, attackRange, playerMask);
        if (playerColInfo != null)
        {
            animator.SetTrigger("Attack");
        }

        ratColInfo = Physics2D.OverlapCircle(pos, attackRange, ratMask);
        if (ratColInfo != null)
        {
            animator.SetTrigger("Attack");
        }

    }


    public void Attack()
    {
        if (obstacleColInfo != null)
        {
            AudioManager.instance.PlayAudio("Spider", "WoodHit" + Random.Range(1,4));
            obstacleColInfo.gameObject.GetComponent<Destructible>().Destroy();
        }


        if (playerColInfo != null)
        {
            AudioManager.instance.PlayAudio("Spider", "PlayerHit");
            playerColInfo.GetComponent<Health>().TakeDamage(5);
            animator.SetBool("Idle",true);
            speed = 0;
        }

        if (ratColInfo != null)
        {
            AudioManager.instance.PlayAudio("Spider", "PlayerHit");
            ratColInfo.GetComponent<PatrolEnemy>().TakeDamage(100);
        }


        if (playerColInfo == null && obstacleColInfo == null && ratColInfo != null)
        {
            AudioManager.instance.PlayAudio("Spider", "MissHit");
        }
    }

    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(pos, attackRange);
    }

    public void PlayWalk()
    {
        animator.Play("Walk");
        AudioManager.instance.PlayAudio("SpiderMove", "Move");
    }
}
