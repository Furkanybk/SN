using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum TransitionParameters
{
    Sprint,
    Slide,
    Death,
}
public class NinjaController : MonoBehaviour
{
    public Animator animator;

    public float Speed = 2f;
    public float TurnSmoothTime = 0.1f;
    private float TurnSmoothVelocity = 0;

    [HideInInspector]
    public bool W;
    [HideInInspector]
    public bool S;
    [HideInInspector]
    public bool A;
    [HideInInspector]
    public bool D;

    public bool IsSlideArea; 

    [SerializeField] private Text CheckPointText; 

    private Rigidbody rigid;
    public Rigidbody RIGID_BODY
    {
        get
        {
            if (rigid == null)
            {
                rigid = GetComponent<Rigidbody>();
            }
            return rigid;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Orc")
        {
            Debug.Log("Died");
            animator.SetBool(TransitionParameters.Death.ToString(), true);
            return;
        }  

        if(collision.collider.gameObject.layer == 8)
        {
            RIGID_BODY.velocity = Vector3.zero;
            CheckPointText.text = "YOU GOT THIS";
            CheckPointText.enabled = true;
            StartCoroutine(closeText(2f));
            Debug.Log("Checkpoint");
            return;
        }

        if (collision.collider.gameObject.layer == 13)
        { 
            FindObjectOfType<GameMenu>().Complete();
            return;
        }
    }

    // isSlideAre Old:
    //private void OnTriggerEnter(Collider other) 
    //{
    //    if (!IsSlideArea && other.gameObject.tag == "SlideCollider")
    //    {
    //        IsSlideArea = true;
    //    }
    //    else
    //    {
    //        IsSlideArea = false;
    //    }
    //}

    private void OnCollisionStay(Collision collision)
    {
        if (IsSlideArea && (collision.collider.gameObject.layer == 8 || collision.collider.gameObject.layer == 12 || collision.collider.gameObject.layer == 13))
        {
            Debug.Log("Not Sliding.");
            IsSlideArea = false;
            return;
        }
        if (!IsSlideArea && collision.collider.gameObject.layer == 10)
        {
            Debug.Log("Sliding.");
            IsSlideArea = true;
            return;
        }
    }

    private void FixedUpdate()
    {
        float horizontal = 0;
        if (D) horizontal = 1;
        else if (A) horizontal = -1;

        float vertical = 0;
        if (W) vertical = 1;
        else if (S) vertical = -1;

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref TurnSmoothVelocity, TurnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            RIGID_BODY.MovePosition(RIGID_BODY.position + moveDir.normalized * Speed * Time.fixedDeltaTime);
        }
    }

    private IEnumerator closeText(float time)
    {
        yield return new WaitForSeconds(time);
        CheckPointText.enabled = false;
    }
} 