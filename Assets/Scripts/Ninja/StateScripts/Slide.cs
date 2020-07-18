using System.Collections;
using System.Collections.Generic; 
using UnityEngine;

public class Slide : StateMachineBehaviour
{
    public float Speed = 6f;
    public float TurnSmoothTime = 0.1f;
    float TurnSmoothVelocity;
    NinjaController ninja; 

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ninja = animator.GetComponentInParent<NinjaController>();  
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    { 
        if (!ninja.IsSlideArea)
        {
            animator.SetBool(TransitionParameters.Slide.ToString(), false); 
        }
         
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");


        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(ninja.transform.eulerAngles.y, targetAngle, ref TurnSmoothVelocity, TurnSmoothTime);
            ninja.transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward; 
        }

        ninja.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {  

    } 
} 