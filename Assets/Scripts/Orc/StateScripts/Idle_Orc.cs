using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_Orc : StateMachineBehaviour
{
    private OrcController orc;
    [SerializeField]
    private bool moved = false;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        orc = animator.GetComponentInParent<OrcController>();
        moved = false;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!moved && !orc.Idle || orc.WaitTime <= 0)
        {
            moved = true;
            orc.newMoveSpot();
            animator.SetBool(T_Parameters.Running.ToString(), true);
        }
        else
        {
            orc.WaitTime -= Time.deltaTime;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        moved = false;
    }
}
