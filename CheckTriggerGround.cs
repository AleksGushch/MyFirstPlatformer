using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTriggerGround : MonoBehaviour
{
    [SerializeField] private float timerForAlive;
    private float currentTimer;
    private bool isGround;
    private void Start()
    {
        currentTimer = 0;
        isGround = false;
    }

    private void Update()
    {
        if (isGround)
        {
            currentTimer += Time.deltaTime;
            if (currentTimer >= timerForAlive)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Damageable"))
        {
            isGround = true;
        }
    }
}
