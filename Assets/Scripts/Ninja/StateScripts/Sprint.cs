using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprint : StateMachineBehaviour
{
    public float Speed = 2f;
    public float TurnSmoothTime = 0.1f;
    //float TurnSmoothVelocity;

    private NinjaController ninja;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ninja = animator.GetComponentInParent<NinjaController>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!ninja.W && !ninja.A && !ninja.S && !ninja.D)
        {
            animator.SetBool(TransitionParameters.Sprint.ToString(), false);
            return;
        }

        if (ninja.IsSlideArea)
        {
            animator.SetBool(TransitionParameters.Slide.ToString(), true);
            return;
        }

        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");
        Debug.Log(vertical + " " + horizontal);


        //Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        //if (direction.magnitude >= 0.1f)
        //{
        //    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        //    float angle = Mathf.SmoothDampAngle(ninja.transform.eulerAngles.y, targetAngle, ref TurnSmoothVelocity, TurnSmoothTime);
        //    ninja.transform.rotation = Quaternion.Euler(0f, angle, 0f);

        //    Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

        //    ninja.RIGID_BODY.MovePosition(ninja.RIGID_BODY.position + moveDir.normalized * Speed * Time.fixedDeltaTime);
        //}
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}