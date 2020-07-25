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
    public GameObject MoveSpotAsset;
    [Space]
    public Transform MoveSpot;
    public float Speed = 3.5f;
    public float WaitTime = 1.75f;
    public float StartWaitTime;
    public float RandomizeRange = 0.5f;
    [Space]
    private Vector2 Min = Vector2.zero;
    private Vector2 Max = Vector2.zero;
    public float minMoveDistance = 1;

    public bool Idle = true;

    public void Setup(Vector2 min, Vector2 max)
    {
        //gameObject.layer = 2;

        Speed = 3.5f;
        WaitTime = 1.75f;
        StartWaitTime = WaitTime;

        Min = min;
        Max = max;
        minMoveDistance = 1;

        newMoveSpot();
    }

    public void Setup(Vector2 min, Vector2 max, float speed, float waitTime)
    {
        //gameObject.layer = 2;

        Speed = speed;
        WaitTime = waitTime;

        Min = min;
        Max = max;
        minMoveDistance = 1;

        newMoveSpot();
    }

    public void newMoveSpot()
    {
        if (MoveSpot == null)
        {
            MoveSpot = Instantiate(MoveSpotAsset, transform.position, Quaternion.identity, transform.parent).transform;
            MoveSpot.name = name + " Move Spot";
        }

        do
        {
            MoveSpot.position = new Vector3(Random.Range(Min.x, Max.x), transform.position.y, Random.Range(Min.y, Max.y));
        } while (Vector3.Distance(MoveSpot.transform.position, transform.position) < minMoveDistance);

        WaitTime = Random.Range(StartWaitTime - RandomizeRange, StartWaitTime + RandomizeRange) / 2; // Start Moving Fast at he Beginning.
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

    private void OnCollisionEnter(Collision collision)
    {
        //TODO : Yeni hedef konum Orc'un arkasında olacak.
        if (collision.gameObject != gameObject)
        {
            if (!Idle && collision.gameObject.CompareTag("Orc"))
            {
                newMoveSpot();
            }
        }
        else
        {
            Debug.Log("Kendine Çarptı Manyak.");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject != gameObject)
        {
            if (!Idle && other.transform.Equals(MoveSpot))
            {
                Idle = true;
            }
        }
    }
}
