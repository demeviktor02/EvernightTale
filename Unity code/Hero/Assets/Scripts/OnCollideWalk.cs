using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollideWalk : MonoBehaviour
{
    public Animator animator;
    public bool isWalking = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            animator.SetBool("IsWalking", !isWalking);

        }
        
    }
}