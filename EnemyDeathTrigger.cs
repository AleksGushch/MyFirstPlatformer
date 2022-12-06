using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stopper"))
        {
            Destroy(gameObject);
        }
    }
}
