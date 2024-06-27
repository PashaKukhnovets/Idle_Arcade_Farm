using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAnimation : MonoBehaviour
{
    private MouseController mouse;
    private MousePursue mousePursue;
    private Animator animator;

    void Start()
    {
        mouse = this.GetComponent<MouseController>();
        animator = this.GetComponent<Animator>();
        mousePursue = this.GetComponent<MousePursue>();
        mousePursue.MouseRun += MouseRun;
        mouse.MouseAttack += MouseAttack;
        mouse.MouseAttackFalse += MouseAttackFalse;
        mouse.MouseDeath += MouseDeath;
    }

    public void MouseRun()
    {
        animator.SetBool("isRunning", true);
    }

    public void MouseAttack()
    {
        animator.SetBool("isAttacking", true);
        animator.SetBool("isRunning", false);
    }

    public void MouseAttackFalse()
    {
        animator.SetBool("isAttacking", false);
        animator.SetBool("isRunning", true);
    }

    public void MouseDeath()
    {
        animator.SetBool("isDeath", true);
    }
}
