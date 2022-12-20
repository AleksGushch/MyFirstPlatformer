using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonAnimator : MonoBehaviour
{
    private Animator animator;
    private BossController bossController;
    private void Start()
    {
        animator = GetComponent<Animator>();
        bossController = GetComponent<BossController>();
    }

    private void Update()
    {
        MeleeAttack();
        Shooting();
    }

    private void MeleeAttack() 
    {
        if (bossController.IsMelee)
            animator.SetBool("isMelee", true);
        else 
            animator.SetBool("isMelee", false);
    }

    private void Shooting() 
    {
        if (bossController.IsShoot)
            animator.SetBool("isShoot", true);
        else
            animator.SetBool("isShoot", false);
    }
}
