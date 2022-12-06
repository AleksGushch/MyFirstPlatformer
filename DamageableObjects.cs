using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableObjects: MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float currentHealth;

    public bool IsAlive => currentHealth > 0;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    private void Update()
    {
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        if (IsAlive == false)
        {
            Destroy(gameObject.GetComponent<DDenemy>());
            Invoke("Death", 0.5f);
        }
    }

    private void Death() 
    {
        Destroy(gameObject);
    }
}
