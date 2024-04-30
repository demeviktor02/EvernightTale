using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollideWalk : MonoBehaviour
{
    public Animator animator;
    public GameObject player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            animator.SetBool("IsWalking", false);
            player.GetComponent<PlayerMovement2>().isWalking = false;
            player.GetComponent<PlayerMovement2>().playerSpeed = 6.3f;
        }
        
    }
}
