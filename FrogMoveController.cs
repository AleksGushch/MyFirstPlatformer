using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMoveController : MonoBehaviour
{
    [SerializeField] private float frogSpeed, timeToRevert;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private const float IDLE_STATE = 0;
    private const float WALK_STATE = 1;
    private const float REVERT_STATE = 2;
    private float currentState, currentTimeToRevert;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        currentState = WALK_STATE;
        currentTimeToRevert = 0;
    }

    private void Update()
    {
        if (currentTimeToRevert >= timeToRevert) 
        {
            currentTimeToRevert = 0;
            currentState = REVERT_STATE;
        }
        switch (currentState) 
        {
            case IDLE_STATE:
                currentTimeToRevert += Time.deltaTime;
                break;
            case WALK_STATE:
                rb.velocity = Vector2.left * frogSpeed;
                break;
            case REVERT_STATE:
                sr.flipX = !sr.flipX;
                frogSpeed *= -1;
                currentState = WALK_STATE;
                break;
        }
        anim.SetFloat("velocity", rb.velocity.magnitude);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stopper")) 
        {
            currentState = IDLE_STATE;
        }
    }
}
