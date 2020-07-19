using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Running_Orc : StateMachineBehaviour
{
    private OrcController orc;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        orc = animator.GetComponentInParent<OrcController>();
        orc.WaitTime = Random.Range(orc.StartWaitTime - orc.RandomizeRange, orc.StartWaitTime + orc.RandomizeRange); // every begining of running.
        orc.Idle = false;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (orc.Idle /*|| Vector3.Distance(orc.transform.position, orc.MoveSpot.position) < 0.2f*/) // Collider Check now.
        {
            animator.SetBool(T_Parameters.Running.ToString(), false);
            return;
        }

        orc.transform.position = Vector3.MoveTowards(orc.transform.position, orc.MoveSpot.position, orc.Speed * Time.deltaTime);
        orc.transform.LookAt(orc.MoveSpot.transform);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
