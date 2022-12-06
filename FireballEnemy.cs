using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballEnemy : MonoBehaviour
{
    [SerializeField] private GameObject fireball;
    [SerializeField] private float fireForce;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float timeOfSpeed;
    private float currentTime;
    private bool isTimer=false;

    private void Update()
    {
        if (isTimer) 
        {
            TimerForEnemyAttack();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        isTimer = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTimer = false;
    }
    private void Shooting()
    {
        GameObject fireballClon = Instantiate(fireball, firePoint.position, Quaternion.identity);
        fireballClon.GetComponent<Rigidbody2D>().velocity = new Vector2(-fireForce, fireballClon.GetComponent<Rigidbody2D>().velocity.y);
        fireballClon.GetComponent<SpriteRenderer>().flipX = false;
    }

    private void TimerForEnemyAttack() 
    {
        if (currentTime >= timeOfSpeed)
        {
            Shooting();
            currentTime = 0;
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }
}
