using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollideWalk : MonoBehaviour
{
    public Animator animator;
    public GameObject player;
    public bool walkTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            if (walkTrigger == true)
            {
                animator.SetBool("IsWalking", true);
                player.GetComponent<PlayerMovement2>().isWalking = true;
                player.GetComponent<PlayerMovement2>().playerSpeed = player.GetComponent<PlayerMovement2>().walkingSpeed;
            }
            else
            {
                animator.SetBool("IsWalking", false);
                player.GetComponent<PlayerMovement2>().isWalking = false;
                player.GetComponent<PlayerMovement2>().playerSpeed = player.GetComponent<PlayerMovement2>().runningSpeed;
            }

        }
        
    }
}
