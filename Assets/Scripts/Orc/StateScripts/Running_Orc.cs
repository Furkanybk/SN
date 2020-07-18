using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Running_Orc : StateMachineBehaviour
{ 
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    { 

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        OrcController orc = animator.GetComponentInParent<OrcController>();
          
        orc.transform.position = Vector3.MoveTowards(orc.transform.position, orc.MoveSpot.position, orc.Speed * Time.deltaTime);
        orc.transform.LookAt(orc.MoveSpot.transform);

        if (Vector3.Distance(orc.transform.position, orc.MoveSpot.position) < 0.2f)
        {
            animator.SetBool(T_Parameters.Running.ToString(), false); 
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
