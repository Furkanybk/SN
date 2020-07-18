using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    { 

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NinjaController ninja = animator.GetComponentInParent<NinjaController>();

        if(ninja.W || ninja.A || ninja.S || ninja.D)
        {
            animator.SetBool(TransitionParameters.Sprint.ToString(), true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    } 
}
