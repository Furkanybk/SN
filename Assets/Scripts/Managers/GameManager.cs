using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager current = null;

    public Vector3 PlayerLastCheckPoint;
    public static int TotalChance = 3;
    public int RemainingChance = TotalChance;
    public int ReachedCheckPoint = 0;
    public GameObject Ninja;
    public Sprite X_Ninja_Head;

    public TextMeshProUGUI Text_CheckPoint;
    public GameObject UX_RespawnNumber;
    public TextMeshProUGUI Text_ReachedCheckPoint;
     
    [HideInInspector]
    public string CheckPointInfo = "Welcome Ninja Slide";

    private void Awake()
    {
        if(current != null)
        {
            //Destroy(current);
            Debug.Log("Already a GameManager Working.");
        }
        else
        {
            current = this;
        }
    }

    private void OnDestroy()
    {
        if (current == this)
            current = null;
    }

    private void Start()
    {
        SpawnPlayer(Vector3.zero); 
    }

    public void Respawn()
    {
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        SpawnPlayer(PlayerLastCheckPoint);

        switch (RemainingChance)
        { 
            case 1:
                CheckPointInfo = "LAST CHANCE";
                break;
            case 2:
                CheckPointInfo = "WATCH OUT";
                break;
            case 3:
                CheckPointInfo = "OOPS";
                break;
            default:
                break;
        } 
        --RemainingChance; 
        UX_RespawnNumber.transform.GetChild(0).gameObject.transform.GetChild(RemainingChance).GetComponent<Image>().sprite = X_Ninja_Head;

    }

    private void SpawnPlayer(Vector3 pos)
    {
        pos.y = 0.2f;
        GameObject ninja = Instantiate(Ninja, pos, Quaternion.identity);
        SetCamera(ninja);
    }
    private void SetCamera(GameObject ninja)
    {
        CinemachineStateDrivenCamera camera = GameObject.FindGameObjectWithTag("Camera").gameObject.GetComponent<CinemachineStateDrivenCamera>();
        camera.Follow = ninja.transform;
        camera.LookAt = ninja.transform; 
        camera.m_AnimatedTarget = ninja.GetComponentInChildren<Animator>();
    }
}
