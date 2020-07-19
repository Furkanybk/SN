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
    public float RandomizeRange = 0.5f;

    public Animator animator;
    public Material material;
    public Transform MoveSpot;

    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    public bool Idle = true;
     
    public void Setup()
    { 
        WaitTime = Random.Range(StartWaitTime - RandomizeRange, StartWaitTime + RandomizeRange) / 2; // Start Moving Fast at he Beginning.

        gameObject.layer = 2;
        MoveSpot.position = new Vector3(Random.Range(minX, maxX), transform.position.y, Random.Range(minZ, maxZ));
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

    private void OnTriggerStay(Collider other)
    {
        if (!Idle && other.transform.Equals(MoveSpot))
        {
            Idle = true;
        }
    }
}
