using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprint : StateMachineBehaviour
{  
    public float Speed = 2f; 
    public float TurnSmoothTime = 0.1f;
    float TurnSmoothVelocity;
     

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    { 

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NinjaController ninja = animator.GetComponentInParent<NinjaController>();

        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");


        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(ninja.transform.eulerAngles.y, targetAngle, ref TurnSmoothVelocity, TurnSmoothTime);
            ninja.transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            ninja.RIGID_BODY.MovePosition(ninja.RIGID_BODY.position + moveDir.normalized * Speed * Time.fixedDeltaTime);
        }


        if (!ninja.W && !ninja.A && !ninja.S && !ninja.D)
        {
            animator.SetBool(TransitionParameters.Sprint.ToString(), false);
        }

        if (ninja.IsSlideArea)
        {
            animator.SetBool(TransitionParameters.Slide.ToString(), true);
        } 
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    } 
} 