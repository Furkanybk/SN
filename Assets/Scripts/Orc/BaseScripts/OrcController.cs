using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public enum T_Parameters
{
    Running, 
}

public class OrcController : MonoBehaviour
{
    public float Speed;
    public float WaitTime;
    public float StartWaitTime;

    public Animator animator;
    public Material material;
    public Transform MoveSpot;
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
     
    public void Setup()
    { 
        WaitTime = StartWaitTime;

        gameObject.layer = 2;
        MoveSpot.position = new Vector3(Random.Range(minX, maxX), 0.2874999f, Random.Range(minZ, maxZ));
    }

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

}
