using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private Transform boss;
    [SerializeField] private LayerMask layer, layerWall;
    [SerializeField] private float speed, timeForMove, timeForStop, timeForRevert, distance, speedOfMelee, timeForShooting;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private bool isMelee, isMove, isShoot;
    private float coefDetect = 2.5f, currentMeleeTime, currentMoveTime, currentTimeToRevert, currentShootingTime;
    private RaycastHit2D meleeLeft, meleeRight;

    public bool IsMelee => isMelee;
    public bool IsShoot => isShoot;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        isMelee = false;
        isMove = false;
    }

    private void Update()
    {
        MeleeAttack();
        DetectionDistance();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(boss.position, boss.position+Vector3.left * distance);
        Gizmos.DrawLine(boss.position, boss.position+Vector3.right * distance);
        Gizmos.DrawWireSphere(transform.position, distance * coefDetect);
    }

    private void MeleeAttack() 
    {
        meleeLeft = Physics2D.Raycast(boss.position, Vector2.left, distance, layer);
        meleeRight = Physics2D.Raycast(boss.position, Vector2.right, distance, layer);
        if (meleeLeft)
        {
            sr.flipX = false;
            SpeedOfMelee();
        }
        else if (meleeRight) 
        {
            sr.flipX = true;
            SpeedOfMelee();
        }
        else if (isMelee)
        {
            currentMeleeTime += Time.deltaTime;
            if (currentMeleeTime >= 1.2f)
            {
                isMelee = false;
            }
        }
    }

    private void SpeedOfMelee() 
    {
        currentMeleeTime += Time.deltaTime;
        if (currentMeleeTime < 1.2f) 
        {
            isMelee = true;
        }
        if (currentMeleeTime >= 1.2f)//время анимации атаки(ближний бой)
        {
            isMelee = false;
        }
        if (currentMeleeTime >= speedOfMelee)
        {
            currentMeleeTime = 0;
        }
    }

    private void DetectionDistance()
    {
        RaycastHit2D detectLeft = Physics2D.Raycast(boss.position, Vector2.left, distance * coefDetect, layer);
        RaycastHit2D detectLeftWall = Physics2D.Raycast(boss.position, Vector2.left, distance * coefDetect, layerWall);
        RaycastHit2D detectRight = Physics2D.Raycast(boss.position, Vector2.right, distance * coefDetect, layer);
        RaycastHit2D detectRightWall = Physics2D.Raycast(boss.position, Vector2.right, distance * coefDetect, layerWall);
        if (detectLeft && !meleeLeft)
        {
            MoveTimer();
            if (isMove)
            {
                sr.flipX = false;
                rb.velocity = Vector2.left * speed;
            }
        }
        else if (detectRight && !meleeRight)
        {
            MoveTimer();
            if (isMove)
            {
                sr.flipX = true;
                rb.velocity = Vector2.right * speed;
            }
        else
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
                currentShootingTime += Time.deltaTime;
                if (currentShootingTime < 1.2f)
                    isShoot = true;
                if (currentShootingTime >= 1.2f)
                    isShoot = false;
                if (currentShootingTime >= timeForShooting) 
                {
                    currentShootingTime = 0;
                }
            }
        }
        if (detectLeftWall && !detectLeft) 
        {
            currentTimeToRevert += Time.deltaTime;
            if (currentTimeToRevert >= timeForRevert)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
                sr.flipX = true;
            }
        }

    }

    private void MoveTimer()
    {
        currentMoveTime += Time.deltaTime;
        if (currentMoveTime < timeForMove)
        {
            isMove = true;
        }
        if (currentMoveTime >= timeForMove)
        {
            isMove = false;
        }
        if (currentMoveTime >= timeForStop)
        {
            currentMoveTime = 0;
        }
    }
}
