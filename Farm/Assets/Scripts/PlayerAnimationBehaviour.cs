using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationBehaviour : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerController player;

    private void Start()
    {
        player.PlayerIdle += PlayerIdle;
        player.PlayerRun += PlayerRun;
        player.PlayerAttack += PlayerAttack;
        player.PlayerRunAttack += PlayerRunAttack;
        player.PlayerGathering += PlayerGathering;
        player.PlayerDeath += PlayerDeath;
    }

    public void PlayerRun()
    {
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsRunAttacking", false);
        animator.SetBool("IsGathering", false);
        animator.SetBool("IsRunning", true);
    }

    public void PlayerAttack()
    {
        animator.SetBool("IsRunning", false);
        animator.SetBool("IsRunAttacking", false);
        animator.SetBool("IsGathering", false);
        animator.SetBool("IsAttacking", true);
    }

    public void PlayerIdle()
    {
        animator.SetBool("IsRunAttacking", false);
        animator.SetBool("IsRunning", false);
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsGathering", false);
    }

    public void PlayerDeath()
    {
        animator.SetBool("IsRunning", false);
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsRunAttacking", false);
        animator.SetBool("IsGathering", false);
        animator.SetBool("IsDeath", true);
    }

    public void PlayerRunAttack()
    {
        animator.SetBool("IsRunning", false);
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsGathering", false);
        animator.SetBool("IsRunAttacking", true);
    }

    public void PlayerGathering() {
        animator.SetBool("IsRunning", false);
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsRunAttacking", false);
        animator.SetBool("IsGathering", true);
    }
}
