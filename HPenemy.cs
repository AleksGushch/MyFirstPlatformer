using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPenemy : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private Animator anim;
    private float currentHealth;

    public bool IsAlive => currentHealth > 0;

    private void Awake()
    {
        anim = GetComponent<Animator>();
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
            anim.SetBool("isAlive", false);
            Invoke("Death", 0.5f);
        }
    }

    private void Death() 
    {
        Destroy(gameObject);
    }
}
