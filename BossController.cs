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
    private HP hp;

    private bool isMelee, isMove, isShoot, getShootLH, getShootRH, isAOE;
    private float coefDetect = 2.5f, currentMeleeTime, currentMoveTime, currentTimeToRevert, currentShootingTime, currentAOEtime;
    private RaycastHit2D meleeLeft, meleeRight;

    private bool firstLine = false;
    private bool secondLine = false;
    private bool thirdLine = false;

    public bool IsMelee => isMelee;
    public bool IsShoot => isShoot;
    public bool IsAOE => isAOE;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        hp = GetComponent<HP>();
        isMelee = false;
        isMove = false;
        getShootLH = false;
        getShootRH = false;
        isShoot = false;
        isAOE = false;
    }

    private void Update()
    {
        MeleeAttack();
        DetectionDistance();
        HealthTresholds();
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
            getShootLH = true;
        }
        else if (detectRight && !meleeRight)
        {
            MoveTimer();
            if (isMove)
            {
                sr.flipX = true;
                rb.velocity = Vector2.right * speed;
            }
            getShootRH = true;
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
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
        else if (detectRightWall && !detectRight) 
        {
            currentTimeToRevert += Time.deltaTime;
            if (currentTimeToRevert >= timeForRevert)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
                sr.flipX = false;
            }
        }
        if (!detectLeft && getShootLH)
        {
            isShoot = true;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
            currentShootingTime += Time.deltaTime;
            if (currentShootingTime >= 1.2f) 
            {
                isShoot = false;
                getShootLH = false;
                currentShootingTime = 0;
            }
        }
        if (!detectRight && getShootRH) 
        {
            isShoot = true;
            currentShootingTime += Time.deltaTime;
            if (currentShootingTime >= 1.2f)
            {
                isShoot = false;
                getShootRH = false;
                currentShootingTime = 0;
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

    private void HealthTresholds() 
    {
        if (hp.GetHP < 0.75f && !firstLine)
        {
            AOE1();
        }
        else if (hp.GetHP < 0.5f && !secondLine)
        {
            AOE2();
            
        }
        else if (hp.GetHP < 0.25f && !thirdLine) 
        {
            AOE3();
        }
    }

    private void AOE1()
    {
        currentAOEtime += Time.deltaTime;
        if (currentAOEtime < 1.3f)
        {
            isAOE = true;
            isMove = false;
        }
        if (currentAOEtime >= 1.3f)
        {
            isAOE = false;
            isMove = true;
            currentAOEtime = 0;
            firstLine = true;
        }
    }

    private void AOE2()
    {
        currentAOEtime += Time.deltaTime;
        if (currentAOEtime < 1.3f)
        {
            isAOE = true;
            isMove = false;
        }
        if (currentAOEtime >= 1.3f)
        {
            isAOE = false;
            isMove = true;
            currentAOEtime = 0;
            secondLine = true;
        }
    }
    private void AOE3()
    {
        currentAOEtime += Time.deltaTime;
        if (currentAOEtime < 1.3f)
        {
            isAOE = true;
            isMove = false;
        }
        if (currentAOEtime >= 1.3f)
        {
            isAOE = false;
            isMove = true;
            currentAOEtime = 0;
            thirdLine = true;
        }
    }
}
