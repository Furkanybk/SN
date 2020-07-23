using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{  
    public Vector3 PlayerLastCheckPoint;

    public GameObject Ninja;

    public TextMeshProUGUI CheckPointText;

    public string CheckPointInfo = "Welcome Ninja Slide";
     

    private void Start()
    {
        SpawnPlayer(Vector3.zero); 
    }

    public void Respawn()
    {
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        SpawnPlayer(PlayerLastCheckPoint);
        CheckPointInfo = "LAST CHANCE";
        FindObjectOfType<GameMenu>().DeadMenuUI.SetActive(false);
    }

    private void SpawnPlayer(Vector3 pos)
    {
        GameObject ninja = Instantiate(Ninja, Vector3.zero, Quaternion.identity);
        SetCamera(ninja);
    }
    private void SetCamera(GameObject ninja)
    {
        CinemachineStateDrivenCamera camera = GameObject.FindGameObjectWithTag("Camera").gameObject.GetComponent<CinemachineStateDrivenCamera>();
        camera.Follow = ninja.transform;
        camera.LookAt = ninja.transform; 
    }
}
