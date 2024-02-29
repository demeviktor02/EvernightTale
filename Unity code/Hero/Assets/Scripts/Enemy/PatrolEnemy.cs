using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Timeline;

public class PatrolEnemy : Enemy
{
    public float speed;
    public Transform[] patrolPoints;
    public float waitTime;
    public float distance;
    public float minimumDistance;
    public float playerDistance;
    public float attackDistance;
    public int currentPointIndex;
    public GameObject player;
    public Transform target;

    public bool isFlipped = false;

    public bool once = false;
    public bool idle = false;

    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;

    // Update is called once per frame
    void Update()
    {
        
        distance = Mathf.Abs(transform.position.x - patrolPoints[currentPointIndex].position.x);

        if (SeePlayer())
        {
            target = player.transform;
            if (Mathf.Abs(transform.position.x - player.transform.position.x) > attackDistance)
            {
                animator.SetBool("IsWalking", true);
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                LookAtTarget();

            }
            else
            {
                animator.SetTrigger("Attack");
            }
            
        }        
        else
        {
            target = patrolPoints[currentPointIndex];
            if (Mathf.Abs(transform.position.x - patrolPoints[currentPointIndex].position.x) > minimumDistance)  //Vector2.Distance(transform.position, patrolPoints[currentPointIndex].position) > minimumDistance
            {
                if (idle == false)
                {
                    animator.SetBool("IsWalking", true);
                    transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);
                    LookAtTarget();
                }

            }
            else
            {

                if (once == false)
                {
                    animator.SetBool("IsWalking", false);
                    idle = true;
                    once = true;
                    StartCoroutine(Wait());
                }

            }
        }

    }

    IEnumerator Wait()
    {
        
        yield return new WaitForSeconds(waitTime);

        if (currentPointIndex + 1 < patrolPoints.Length)
        {
            currentPointIndex++;
        }
        else
        {
            currentPointIndex = 0;
        }
        once = false;
        idle = false;

    }

    public bool SeePlayer()
    {
        if (Mathf.Abs(transform.position.x - player.transform.position.x) < playerDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void LookAtTarget()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > target.position.x && isFlipped)
        {
            animator.SetTrigger("Turn");
            transform.localScale = flipped;
            isFlipped = false;

        }
        else if (transform.position.x < target.position.x && !isFlipped)
        {
            animator.SetTrigger("Turn");
            transform.localScale = flipped;
            isFlipped = true;
        }
    }

    public void LookAtTargetTrigger()
    {
        transform.Rotate(0f, 180f, 0f);
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

    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(pos, attackRange);
    }

    public override void Die()
    {
        SaveData.instance.playerData.SlayedEnemies++;
        deathParticles.Play();
        Destroy(gameObject);
    }
}
