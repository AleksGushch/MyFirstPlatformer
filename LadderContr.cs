using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderContr : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    private PlayerController playerController;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody2D>();
        playerController = player.GetComponent<PlayerController>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController.IsLadder = true;
            rb.gravityScale = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerController.IsLadder = false;
        rb.gravityScale = 1;
    }
}
