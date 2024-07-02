using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointTrigger : MonoBehaviour
{
    public Vector2 otherSide;
    public bool isSpiderActive;
    public GameObject spider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.instance.SpawnPoint = otherSide;
            if (isSpiderActive == true)
            {
                spider.GetComponent<BoxCollider2D>().enabled = true;
                spider.GetComponent<FollowEnemy>().enabled = true;
                spider.GetComponent<Rigidbody2D>().simulated = true;
                spider.GetComponent<Animator>().Play("Walk");
            }
        }
        
    }
}
