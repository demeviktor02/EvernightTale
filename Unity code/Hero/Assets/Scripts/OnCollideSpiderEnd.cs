using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollideSpiderEnd : MonoBehaviour
{
    public FollowEnemy spider;
    public Animator animator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Spider" && spider.playerColInfo == null)
        {
            spider.speed = 0;
            animator.SetBool("SitDown", true);
            animator.Play("Idle");
        }

    }
}
