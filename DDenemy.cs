using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDenemy : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out HP hp))
        {
            hp.TakeDamage(damage);
            Destroy(gameObject);
        }
        //else if (collision.CompareTag("Ground"))
        //{
        //    Destroy(gameObject);
        //}
    }
}
