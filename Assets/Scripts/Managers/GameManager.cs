using Boo.Lang;
using Cinemachine;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager current = null; 

    public CheckPointManager lastCheckpoint;
    public float CameraLastPathWay; 
     
    public Vector3 PlayerLastCheckPoint; 
    public static int TotalChance = 3;
    public int RemainingChance = TotalChance; 
    public int ReachedCheckPoint = 0;
    [Space] 
    public CinemachineVirtualCamera camera; 
    [Space]
    public GameObject Ninja;
    [Space]
    public TextMeshProUGUI Text_CheckPoint;
    public GameObject UX_RespawnNumber;
    public TextMeshProUGUI Text_ReachedCheckPoint;

    [HideInInspector]
    public bool IsRespawned = false;

    [HideInInspector]
    public string CheckPointInfo;

    private void Awake()
    {  
        if (current != null)
        { 
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
        IsRespawned = true;
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        if (lastCheckpoint)
            SpawnPlayer(lastCheckpoint.transform.position);
        else
            SpawnPlayer(Vector3.zero);

        switch (RemainingChance)
        { 
            case 0:
                CheckPointInfo = "LAST CHANCE";
                break;
            case 1:
                CheckPointInfo = "WATCH OUT";
                break; 
            default:
                break;
        }  
    }

    private void SpawnPlayer(Vector3 pos)
    {
        pos.y += 0.25f;
        GameObject ninja = Instantiate(Ninja, pos, Quaternion.identity);
        ninja.transform.position = pos;
        SetCamera(ninja);
    }
    private void SetCamera(GameObject ninja)
    {
        camera.Follow = ninja.transform;
        camera.LookAt = ninja.transform; 
    }
} 