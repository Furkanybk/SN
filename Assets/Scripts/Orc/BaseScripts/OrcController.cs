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

    [SerializeField]
    private NinjaController enemy = null;
    private bool GaveUp = false;

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
        WaitTime = Random.Range(StartWaitTime - RandomizeRange, StartWaitTime + RandomizeRange) / 2;
        if (MoveSpot == null)
        {
            MoveSpot = Instantiate(MoveSpotAsset, transform.position, Quaternion.identity, transform.parent).transform;
            MoveSpot.name = name + " Move Spot";
        }

        Vector3 position = MoveSpot.position;
        do
        {
            position = new Vector3(Random.Range(Min.x, Max.x), transform.position.y, Random.Range(Min.y, Max.y));
        } while (Vector3.Distance(position, transform.position) < minMoveDistance);

        MoveSpot.position = position;
    }

    public void newMoveSpot(Vector3 spot)
    {
        if (!enemy.IsSlideArea)
        {
            stopAtack();
            return;
        }

        if (MoveSpot == null)
        {
            MoveSpot = Instantiate(MoveSpotAsset, transform.position, Quaternion.identity, transform.parent).transform;
            MoveSpot.name = name + " Move Spot";
        }

        MoveSpot.position = spot;
        WaitTime = 0;
    }

    private void startAtacking()
    {
        Debug.Log("RAWRRR");
        StartCoroutine(atackMove());
        StartCoroutine(giveUp());
    }

    private IEnumerator atackMove()
    {
        newMoveSpot(enemy.transform.position);
        yield return new WaitForSecondsRealtime(0.2f);
        if (enemy)
        {
            StartCoroutine(atackMove());
        }
        else
        {
            StartCoroutine(giveUp());
        }
    }

    private void stopAtack()
    {
        StopAllCoroutines();
        enemy = null;
        GaveUp = true;
        newMoveSpot();
        StartCoroutine(giveUp());
    }

    private IEnumerator giveUp()
    {
        yield return new WaitForSecondsRealtime(Random.Range(3, 4));
        if(!GaveUp)
        {
            stopAtack();
            Debug.Log("Gave Up.");
        }
        else
        {
            GaveUp = false;
            Debug.Log("Can atack again.");
        }
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
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("Killed.");
                stopAtack();
            }
            //else if (!Idle && !Atacking && collision.gameObject.CompareTag("Orc"))
            //{
            //    newMoveSpot();
            //}
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
            if (enemy == null && !GaveUp)
            {
                enemy = other.GetComponent<NinjaController>();
                if(enemy)
                {
                    if (enemy.IsSlideArea && !enemy.animator.GetBool(TransitionParameters.Death.ToString()))
                    {
                        float chance = Random.Range(0f, 1f);
                        if (chance < 0.005f)
                        {
                            startAtacking();
                        }
                        else
                        {
                            enemy = null;
                        }
                    }
                    else
                    {
                        enemy = null;
                        GaveUp = false;
                    }
                }
                
            }

            if (!Idle && other.transform.Equals(MoveSpot))
            {
                Idle = true;
            }
        }
    }
}