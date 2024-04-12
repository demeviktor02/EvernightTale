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

    Collider2D obstacleColInfo;
    Collider2D playerColInfo;

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

    }


    public void Attack()
    {
        if (obstacleColInfo != null)
        {
            Destroy(obstacleColInfo.gameObject);
        }


        if (playerColInfo != null)
        {
            playerColInfo.GetComponent<Health>().TakeDamage(5);
        }
    }

    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(pos, attackRange);
    }
}
