using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

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
    public float runningSpeed;
    public float WaitTime = 1.75f;
    public float StartWaitTime;
    public float RandomizeRange = 0.5f;
    [Space]
    private Vector2 Min = Vector2.zero;
    private Vector2 Max = Vector2.zero;
    public float minMoveDistance = 1;

    public bool Idle = true;

    [SerializeField]
    public NinjaController enemy = null;
    private bool GaveUp = false;

    public int attacktype; // 0 --> Force Attack, 1 --> Wall Attack

    public void Setup(Vector2 min, Vector2 max, int orcAttack)
    { 
        Speed = 3.5f;
        runningSpeed = Speed * 2f;
        WaitTime = 1.75f;
        StartWaitTime = WaitTime;

        Min = min;
        Max = max;
        minMoveDistance = 1;

        attacktype = orcAttack;

        newMoveSpot();
    }

    public void Setup(Vector2 min, Vector2 max, float speed, float waitTime, int orcAttack)
    { 
        Speed = speed;
        runningSpeed = Speed * 1.75f;
        WaitTime = waitTime;

        Min = min;
        Max = max;
        minMoveDistance = 1;

        attacktype = orcAttack;

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
        //Debug.Log("RAWRRR");
        StartCoroutine(atackMove());
        StartCoroutine(giveUp());
    }

    private IEnumerator atackMove()
    {
        newMoveSpot(enemy.transform.position);
        yield return new WaitForFixedUpdate();
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
            //Debug.Log("Gave Up.");
        }
        else
        {
            GaveUp = false;
            //Debug.Log("Can atack again.");
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
        if (collision.gameObject != gameObject)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("Killed.");
                stopAtack();
            }
            if (collision.gameObject.GetComponent<CheckPointManager>())
            {
                stopAtack();
                newMoveSpot();
            }
            #region Condition for WallAttack
            if (collision.gameObject.CompareTag("Wall"))
            {
                Idle = true;
            } 
            #endregion
            else if (!Idle && collision.gameObject.CompareTag("Orc"))
            {
                newMoveSpot();
            }
        }
        else
        {
            Debug.Log("Kendine Çarptı Manyak.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        #region WallAttack
        if(attacktype == 1)
        {
            if (enemy == null && !GaveUp)
            {
                enemy = other.GetComponent<NinjaController>();
                if (enemy)
                {
                    if (enemy.IsSlideArea && enemy.touchingWall && !enemy.animator.GetBool(TransitionParameters.Death.ToString()))
                    {
                        //float chance = Random.Range(0f, 1f);
                        //if (chance < 0.005f)
                        //{
                        startAtacking();
                        //}
                        //else
                        //{
                        //    enemy = null;
                        //}
                    }
                    else
                    {
                        enemy = null;
                        GaveUp = false;
                    }
                }

            }
            Vector3 DistancetoWall;
            if (other.gameObject.CompareTag("Player"))
            {
                DistancetoWall = (other.gameObject.transform.forward * 15f) + other.gameObject.transform.position;
                enemy = other.GetComponent<NinjaController>();
                if (enemy)
                {
                    if (enemy.IsSlideArea && enemy.touchingWall && !enemy.animator.GetBool(TransitionParameters.Death.ToString()))
                    {
                        newMoveSpot(DistancetoWall);
                    }
                }
            }
        } 
        #endregion
    }

    private void OnTriggerStay(Collider other)
    { 
        if (other.gameObject != gameObject)
        { 
            #region ForceAttack
            if (attacktype == 0)
            {
                if (enemy == null && !GaveUp)
                {
                    enemy = other.GetComponent<NinjaController>();
                    if (enemy)
                    {
                        if (enemy.IsSlideArea && enemy.touchingWall && !enemy.animator.GetBool(TransitionParameters.Death.ToString()))
                        {
                            startAtacking();
                        }
                        else
                        {
                            enemy = null;
                            GaveUp = false;
                        }
                    } 
                }
            } 
            #endregion 

            if (!Idle && other.transform.Equals(MoveSpot))
            {
                Idle = true;
            }
        }
    }
}