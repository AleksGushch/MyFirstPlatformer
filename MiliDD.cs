using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiliDD : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out HPenemy hp))
        {
            hp.TakeDamage(damage);
            Destroy(gameObject);
        } 
    }
    private void Update()
    {
        Invoke("DestroySwordArea", 1.0f);
    }

    private void DestroySwordArea() 
    {
        Destroy(gameObject);
    }
}
