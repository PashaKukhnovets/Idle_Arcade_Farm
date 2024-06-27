using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathReleaser : StateMachineBehaviour
{
    private float beginPoint = 0.0f;
    private bool isNextPlaying;

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isNextPlaying = (Time.time - beginPoint) > stateInfo.length / 2.8f;
        if (isNextPlaying)
        {
            animator.GetComponent<CapsuleCollider>().height = 0.93f;
            beginPoint = Time.time;
        }
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        beginPoint = Time.time;
    }
}
