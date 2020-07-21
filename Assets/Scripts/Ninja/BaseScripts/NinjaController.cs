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
    public Material material;
    public bool W;
    public bool S;
    public bool A;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SlideCollider" && !IsSlideArea)
        {
            IsSlideArea = true;
        }
        else
        {
            IsSlideArea = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Orc")
        { 
            animator.SetBool(TransitionParameters.Death.ToString(), true);
        }  

        if(collision.collider.gameObject.layer == 8)
        {
            RIGID_BODY.velocity = Vector3.zero;
            CheckPointText.text = "YOU GOT THIS";
            CheckPointText.enabled = true;
            StartCoroutine(closeText(2f));

        }

        if (collision.collider.gameObject.layer == 13)
        { 
            FindObjectOfType<GameMenu>().Complete();
        }
    } 
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.gameObject.layer == 10 && !IsSlideArea)
        {
            IsSlideArea = true;
        }
        else if (collision.collider.gameObject.layer == 8 || collision.collider.gameObject.layer == 12 || collision.collider.gameObject.layer == 13)
        {
            IsSlideArea = false;
        }
    }
     
    public void ChangeMaterial()
    {
        if (material == null)
        {
            Debug.LogError("No material specified...");
        }

        Renderer[] arrMaterials = this.gameObject.GetComponentsInChildren<Renderer>();

        foreach (Renderer r in arrMaterials)
        {
            if (r.gameObject != this.gameObject)
            {
                r.material = material;
            }
        }
    } 
    private IEnumerator closeText(float time)
    {
        yield return new WaitForSeconds(time);
        CheckPointText.enabled = false;
    }
} 