using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackReleaser : StateMachineBehaviour
{

    private AudioSource shovelAudio;
    private float beginPoint = 0.0f;
    private bool check = true;
    private bool isNextPlaying;

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (check)
        {
            isNextPlaying = (Time.time - beginPoint) > stateInfo.length / 2.7f;
            if (isNextPlaying)
            {
                shovelAudio.Play();
                beginPoint = Time.time;
                check = false;
            }
        }
        else
        {
            isNextPlaying = (Time.time - beginPoint) > stateInfo.length;
            if (isNextPlaying)
            {
                shovelAudio.Play();
                beginPoint = Time.time;
            }
        }

    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        shovelAudio = animator.gameObject.GetComponent<AudioSource>();
        beginPoint = Time.time;
        check = true;
    }

}
