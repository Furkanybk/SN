using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{  
    public Vector3 PlayerLastCheckPoint;
    public int CheckPointChance = 2;
    public int ReachedCheckPoint = 0;
    public GameObject Ninja;

    public TextMeshProUGUI Text_CheckPoint;
    public TextMeshProUGUI Text_RespawnNumber;
    public TextMeshProUGUI Text_ReachedCheckPoint;
     
    [HideInInspector]
    public string CheckPointInfo = "Welcome Ninja Slide";
     

    private void Start()
    {
        SpawnPlayer(Vector3.zero);
        Text_RespawnNumber.text = "Try Left : " + CheckPointChance;
    }

    public void Respawn()
    {
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        SpawnPlayer(PlayerLastCheckPoint);

        switch (CheckPointChance)
        { 
            case 1:
                CheckPointInfo = "LAST CHANCE";
                break;
            case 2:
                CheckPointInfo = "WATCH OUT ORCS :D";
                break;
            default:
                break;
        }
        --CheckPointChance;
        Text_RespawnNumber.text = "Try Left : " + CheckPointChance; 
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
    }
}
