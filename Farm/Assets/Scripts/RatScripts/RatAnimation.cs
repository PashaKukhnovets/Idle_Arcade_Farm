using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatAnimation : MonoBehaviour
{
    private RatController rat;
    private Pursue ratPursue;
    private Animator animator;

    void Start()
    {
        rat = this.GetComponent<RatController>();
        animator = this.GetComponent<Animator>();
        ratPursue = this.GetComponent<Pursue>();
        ratPursue.RatRun += RatRun;
        rat.RatAttack += RatAttack;
        rat.RatAttackFalse += RatAttackFalse;
        rat.RatDeath += RatDeath;
    }

    public void RatRun() {
        animator.SetBool("isRunning", true);
    }

    public void RatAttack() {
        animator.SetBool("isAttacking", true);
        animator.SetBool("isRunning", false);
    }

    public void RatAttackFalse()
    {
        animator.SetBool("isAttacking", false);
        animator.SetBool("isRunning", true);
    }

    public void RatDeath() {
        animator.SetBool("isDeath", true);
    }
}
