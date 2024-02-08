using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : Enemy
{
    public float speed;
    public Transform target;
    public float minimumDistance;
    public float seeDistance;

    public bool seePlayer = true;
    public bool isFlipped = false;

    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;

    void Update()
    {
        SeePlayer();
        LookAtPlayer();

        if (seePlayer == true)
        {
            if (Vector2.Distance(transform.position, target.position) > minimumDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            else
            {
                animator.SetTrigger("Attack");
            }
        }
        
    }

    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<Health>().TakeDamage(1);
        }
    }

    public void SeePlayer()
    {
        if (Vector2.Distance(transform.position, target.position) < seeDistance)
        {
            animator.SetBool("IsSeePlayer", true);
            seePlayer = true;
        }
        else
        {
            animator.SetBool("IsSeePlayer", false);
            seePlayer = false;
        }
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > target.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < target.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
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
