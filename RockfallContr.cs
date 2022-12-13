using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockfallContr : MonoBehaviour
{
    [SerializeField] private Rigidbody2D[] stones;
    private bool isActive;

    void Start()
    {
        isActive = false;
        foreach (Rigidbody2D stone in stones) 
        {
            stone.gravityScale = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            isActive = true;
        }
    }
    
    void Update()
    {
        if (isActive) 
        {
            foreach (Rigidbody2D stone in stones) 
            {
                stone.gravityScale = 1;
                isActive = false;
            }
        }
    }
}
