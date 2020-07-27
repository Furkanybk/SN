﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    [Range(1f, 10f)]
    public float Speed = 10f;
    [Range(0.01f, 1f)]
    public float TurnSmoothTime = 0.685f;

    private float TurnSmoothVelocity = 0; 

    [HideInInspector]
    public float vertical = 0;
    [HideInInspector]
    public float horizontal = 0;
    [HideInInspector]
    public bool IsSlideArea;

    private GameManager gm;

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

    private void Start()
    {
        transform.position = GameManager.current.PlayerLastCheckPoint;
    } 

    private void OnCollisionEnter(Collision collision)
    {
        if (!animator.GetBool(TransitionParameters.Death.ToString()) && collision.gameObject.CompareTag("Orc"))
        {
            Debug.Log("Died");
            animator.SetBool(TransitionParameters.Death.ToString(), true);
            return;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (IsSlideArea && collision.gameObject.GetComponent<CheckPointManager>())
        {
            Debug.Log("Not Sliding.");
            IsSlideArea = false;
            return;
        }
        else if (!IsSlideArea && collision.gameObject.CompareTag("SlideArea"))
        {
            Debug.Log("Sliding.");
            IsSlideArea = true;
            return;
        }
    }

    private void FixedUpdate()
    {
        if (animator.GetBool(TransitionParameters.Death.ToString())) return;

        vertical = SimpleInput.GetAxisRaw("Vertical");
        horizontal = SimpleInput.GetAxisRaw("Horizontal");

        Camera cam = Camera.main;
        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;

        forward.y = 0;
        forward.Normalize();

        right.y = 0;
        right.Normalize();

        Vector3 direction = forward * vertical + right * horizontal;

        if (!IsSlideArea)
        {
            if (horizontal == 0 && vertical == 0) return;
            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref TurnSmoothVelocity, TurnSmoothTime / 5);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                RIGID_BODY.MovePosition(RIGID_BODY.position + moveDir.normalized * (Speed / 1.5f) * Time.fixedDeltaTime);
            }
        }
        else
        {
            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref TurnSmoothVelocity, TurnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        }
    } 
}