using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossController : MonoBehaviour
{
    [SerializeField] private Transform boss;
    [SerializeField] private LayerMask layerPlayer, layerWall;
    [SerializeField] private float speed, timeForMove, timeForStop, timeForRevert, distance, speedOfMelee, timeForShooting;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private HP hp;
    private Fireball fireball;
    private AOEcontrol aOEcontrol;
    private Fighting fighting;

    private bool isMelee, melee, isMove, isShoot, shoot,isAOE, aoe;
    private float currentMeleeTime, currentMoveTime, currentTimeToRevert, currentShootingTime, currentAOEtime;
    private RaycastHit2D meleeLeft, meleeRight;
    private bool[] hpTreshold;
    int hp_cut_off;
    private Vector2 currentDirection;
    
    public bool IsMelee => isMelee;
    public bool IsShoot => isShoot;
    public bool IsAOE => isAOE;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        hp = GetComponent<HP>();
        fireball = GetComponent<Fireball>();
        aOEcontrol = GetComponent<AOEcontrol>();
        fighting = GetComponent<Fighting>();
        isMelee = false;
        melee = false;
        isMove = false;
        isShoot = false;
        shoot = false;
        isAOE = false;
        aoe = false;
        hpTreshold = new bool[GlobalVarNames.HealthTresholds];
        for (int i = 0; i < hpTreshold.Length; i++) 
        {
            hpTreshold[i] = false;
        }
        currentDirection = Vector2.left;
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
        Gizmos.DrawWireSphere(transform.position, distance * GlobalVarNames.CoefficientForDetection);
    }

    private void MeleeAttack() 
    {
        if (boss != null)
        {
            meleeLeft = Physics2D.Raycast(boss.position, Vector2.left, distance, layerPlayer);
            meleeRight = Physics2D.Raycast(boss.position, Vector2.right, distance, layerPlayer);
        }
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
        else if (isMelee)// завершить атаку даже если игрок вышел из зоны ближнего боя
        {
            currentMeleeTime += Time.deltaTime;
            if (currentMeleeTime >= GlobalVarNames.TimeForAttackAnimation)
            {
                isMelee = false;
            }
        }
    }

    private void SpeedOfMelee()// периодичная атака босса если игрок стоит в зоне ближнего боя
    {
        currentMeleeTime += Time.deltaTime;
        if (currentMeleeTime < GlobalVarNames.TimeForAttackAnimation)
        {
            isMelee = true;
            isShoot = false;
        }
        if (currentMeleeTime >= GlobalVarNames.TimeForAttackAnimation * 0.7f)
            FightingZoneRespawn();
        if (currentMeleeTime >= GlobalVarNames.TimeForAttackAnimation)
        {
            isMelee = false;
        }
        if (currentMeleeTime >= speedOfMelee)
        {
            currentMeleeTime = 0;
            melee = false;
        }
    }

    private void DetectionDistance()
    {
        if (boss != null)
        {
            RaycastHit2D detectLeft = Physics2D.Raycast(boss.position, Vector2.left, distance * GlobalVarNames.CoefficientForDetection, layerPlayer);
            RaycastHit2D detectLeftWall = Physics2D.Raycast(boss.position, Vector2.left, distance * GlobalVarNames.CoefficientForDetection, layerWall);
            RaycastHit2D detectRight = Physics2D.Raycast(boss.position, Vector2.right, distance * GlobalVarNames.CoefficientForDetection, layerPlayer);
            RaycastHit2D detectRightWall = Physics2D.Raycast(boss.position, Vector2.right, distance * GlobalVarNames.CoefficientForDetection, layerWall);
            if (detectLeft && !meleeLeft)
            {
                MoveTimer();
                if (isMove)
                {
                    sr.flipX = false;
                    rb.velocity = Vector2.left * speed;
                    currentDirection = Vector2.left;
                }
                else
                {
                    ShootingTime();
                }
            }
            else if (detectRight && !meleeRight)
            {
                MoveTimer();
                if (isMove)
                {
                    sr.flipX = true;
                    rb.velocity = Vector2.right * speed;
                    currentDirection = Vector2.right;
                }
                else
                {
                    ShootingTime();
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
            else if (detectRightWall && !detectRight)
            {
                currentTimeToRevert += Time.deltaTime;
                if (currentTimeToRevert >= timeForRevert)
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
                    sr.flipX = false;
                }
            }
        }
    }

    private void MoveTimer()
    {
        currentMoveTime += Time.deltaTime;
        if (currentMoveTime < timeForMove)
        {
            isMove = true;
            currentShootingTime = 0;
        }
        else if (currentMoveTime >= timeForMove)
        {
            isMove = false;
        }
    }

    private void ShootingTime() 
    {
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        currentShootingTime += Time.deltaTime;
        if (currentShootingTime < timeForShooting)
        {
            isShoot = false;
        }
        else if (currentShootingTime >= timeForShooting)
        {
            isShoot = true;
        }
        if (currentShootingTime >= timeForShooting + GlobalVarNames.TimeForAttackAnimation * 0.6f)
            FireballRespawn();
        if (currentShootingTime >= timeForShooting + GlobalVarNames.TimeForAttackAnimation)
        {
            isShoot = false;
        }
        if (currentShootingTime >= timeForStop) 
        {
            shoot = false;
            currentMoveTime = 0;
        }
    }

    
    private void HealthTresholds() 
    {
        if ((hp.GetHP <= 0.75f && !hpTreshold[0]) || (hp.GetHP <= 0.5f && !hpTreshold[1]) || (hp.GetHP <= 0.25f) && !hpTreshold[2])
        {
            AOE();
        }
        if (!isAOE)
            currentAOEtime = 0;
    }

    private void AOE()
    {
        currentAOEtime += Time.deltaTime;
        if (currentAOEtime < GlobalVarNames.TimeForMassAttackAnimation)
        {
            isAOE = true;
            if (!aoe)
            {
                StartCoroutine(aoeResp());
                aoe = true;
            }
            isMelee = false;
            isShoot = false;
            isMove = false;
        }
        else
        {
            isAOE = false;
            aoe = false;
            hp_cut_off++;
            hpTreshold[hp_cut_off - 1] = true;
        }
    }

    private void FireballRespawn() 
    {
        if (!shoot)
        {
            fireball.Shooting(currentDirection);
            shoot = true;
        }
    }

    private void FightingZoneRespawn() 
    {
        if (!melee) 
        {
            fighting.FightingDirection(currentDirection);
            melee = true;
        }
    }

    private IEnumerator aoeResp() 
    {
        int i=1;
        while (i <= 2)
        {
            yield return new WaitForSeconds(1f);
            aOEcontrol.Shooting();
            i++;
        }
    }
}
