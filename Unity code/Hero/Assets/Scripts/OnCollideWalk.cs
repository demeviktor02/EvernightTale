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
            animator.SetBool("IsWalking", !player.GetComponent<PlayerMovement2>().isWalking);
            player.GetComponent<PlayerMovement2>().isWalking = !player.GetComponent<PlayerMovement2>().isWalking;

            if (player.GetComponent<PlayerMovement2>().isWalking == true)
            {
                player.GetComponent<PlayerMovement2>().playerSpeed = player.GetComponent<PlayerMovement2>().walkingSpeed;
            }
            else
            {
                player.GetComponent<PlayerMovement2>().playerSpeed = player.GetComponent<PlayerMovement2>().playerSpeed;
            }
        }
        
    }
}
