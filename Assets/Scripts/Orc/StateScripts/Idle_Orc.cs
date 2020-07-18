using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_Orc : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        OrcController orc = animator.GetComponentInParent<OrcController>(); 

        if (orc.WaitTime <= 0)
        {
            orc.MoveSpot.position = new Vector3(Random.Range(orc.minX, orc.maxX), 0.2874999f, Random.Range(orc.minZ, orc.maxZ));
            orc.WaitTime = orc.StartWaitTime;
            animator.SetBool(T_Parameters.Running.ToString(), true);
        }
        else
        {
            orc.WaitTime -= Time.deltaTime;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
