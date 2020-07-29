using System.Collections;
using UnityEngine;

public enum T_Parameters
{
    Running, 
}

public enum AttackType
{
    NULL,
    DirectAtack,
    WallAttack,
}

public class OrcController : MonoBehaviour
{
    #region Variables

    public static bool debugging = false;
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

    public AttackType attackType; // 0 --> Force Attack, 1 --> Wall Attack

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

    #endregion

    #region Setup Codes
    public void Setup(Vector2 min, Vector2 max)
    { 
        Speed = 3.5f;
        runningSpeed = Speed * 2f;
        WaitTime = 1.75f;
        StartWaitTime = WaitTime;

        Min = min;
        Max = max;
        minMoveDistance = 1;

        attackType = AttackType.NULL;
        getRandomAttackType();

        newMoveSpot();
    }

    public void Setup(Vector2 min, Vector2 max, float speed, float waitTime)
    { 
        Speed = speed;
        runningSpeed = Speed * 1.75f;
        WaitTime = waitTime;

        Min = min;
        Max = max;
        minMoveDistance = 1;

        attackType = AttackType.NULL;
        getRandomAttackType();

        newMoveSpot();
    }

    private void getRandomAttackType()
    {
        int num = Random.Range(0, 3);
        switch (num)
        {
            case 0:
                attackType = AttackType.NULL;
                break;
            case 1:
                attackType = AttackType.DirectAtack;
                break;
            case 2:
                attackType = AttackType.WallAttack;
                break;
            default:
                Debug.LogWarning("Randomize Error : " + num);
                break;
        }
    }
    #endregion

    #region Move Spot Codes
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

        WaitTime = Random.Range(StartWaitTime - RandomizeRange, StartWaitTime + RandomizeRange) / 2;

        if (MoveSpot == null)
        {
            MoveSpot = Instantiate(MoveSpotAsset, transform.position, Quaternion.identity, transform.parent).transform;
            MoveSpot.name = name + " Move Spot";
        }
        MoveSpot.position = spot;
        WaitTime = 0;
    }
    #endregion

    #region Direct Attack Codes
    private void startAtacking()
    {
        if(debugging)
            Debug.Log(gameObject.name + " is atacking.");
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
        if (!GaveUp)
        {
            stopAtack(); 
            if (debugging)
                Debug.Log(gameObject.name + " gave up.");
        }
        else
        {
            GaveUp = false;

            if (debugging)
                Debug.Log(gameObject.name + " can atack again.");
        }
    }
    #endregion

    #region Collision Trigger

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != gameObject)
        {
            if (attackType == AttackType.DirectAtack)
            {
                if (collision.gameObject.CompareTag("Player"))
                {
                    if (debugging)
                        Debug.Log(gameObject.name + " killed the player.");
                    stopAtack();
                }
                
            }
            if (collision.gameObject.GetComponent<CheckPointManager>())
            {
                stopAtack();
                newMoveSpot();
            }
            if (collision.gameObject.CompareTag("Wall"))
            {
                Idle = true;
                StopAllCoroutines();
                enemy = null;
                GaveUp = true;
                StartCoroutine(giveUp());
            } 
            if (!Idle && collision.gameObject.CompareTag("Orc"))
            {
                newMoveSpot();
            }
        }
        else
        {
            if (debugging)
                Debug.Log("Kendine Çarptı Manyak.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        #region [Direct Attack] (commented)
        //if (attackType == AttackType.DirectAtack)
        //{
        //    if (enemy == null && !GaveUp)
        //    {
        //        enemy = other.GetComponent<NinjaController>();
        //        if (enemy)
        //        {
        //            if (enemy.IsSlideArea && enemy.touchingWall && !enemy.animator.GetBool(TransitionParameters.Death.ToString()))
        //            {
        //                //float chance = Random.Range(0f, 1f);
        //                //if (chance < 0.005f)
        //                //{
        //                startAtacking();
        //                //}
        //                //else
        //                //{
        //                //    enemy = null;
        //                //}
        //            }
        //            else
        //            {
        //                enemy = null;
        //                GaveUp = false;
        //            }
        //        }

        //    }
        //}
        #endregion

        #region WallAttack
        if (attackType == AttackType.WallAttack)
        {
            Vector3 DistancetoWall;
            enemy = other.GetComponent<NinjaController>();
            if (enemy)
            {
                DistancetoWall = (other.gameObject.transform.forward * 15f) + other.gameObject.transform.position;
                if (enemy.IsSlideArea && enemy.touchingWall && !enemy.animator.GetBool(TransitionParameters.Death.ToString()))
                {
                    newMoveSpot(DistancetoWall);
                    if (debugging)
                        Debug.Log(gameObject.name + " is wall atacking.");
                }
            }
        }
        #endregion

    }

    private void OnTriggerStay(Collider other)
    { 
        if (other.gameObject != gameObject)
        {
            #region [Direct Attack]
            if (attackType == AttackType.DirectAtack)
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

    #endregion
}