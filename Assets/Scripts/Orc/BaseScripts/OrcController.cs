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
    public Animator animator;
    public Transform MoveSpot;
    [Space]
    public float Speed;
    public float WaitTime;
    public float StartWaitTime;
    public float RandomizeRange = 0.5f;
    [Space]
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

    private void OnTriggerEnter(Collider other)
    {
        if (!Idle && other.transform.Equals(MoveSpot))
        {
            Idle = true;
        }
    }

}
