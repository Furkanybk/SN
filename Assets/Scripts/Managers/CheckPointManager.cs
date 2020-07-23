using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheckPointManager : MonoBehaviour
{
    private GameManager gm;
    private bool IsCheckpointPassed = false;
    private void Start()
    {
        //TODO : GameManager classına static kendisini ekle.
        // Tagden bulmak yerine GameManager.current diye seslen.
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            gm.PlayerLastCheckPoint = transform.position;
            
            if(this.gameObject.layer != 12)
            {
                if(!IsCheckpointPassed)
                {
                    IsCheckpointPassed = true;
                    gm.ReachedCheckPoint++;
                }
                gm.Text_ReachedCheckPoint.text = "CheckPoint Saved : " + gm.ReachedCheckPoint;

                if (GameObject.FindGameObjectWithTag("Player").transform.position.z != gm.PlayerLastCheckPoint.z)
                {
                    switch (gm.ReachedCheckPoint)
                    {
                        case 1: 
                            gm.CheckPointInfo = "GOOD";
                            break;
                        case 2: 
                            gm.CheckPointInfo = "NICE";
                            break;
                        case 3: 
                            gm.CheckPointInfo = "YOU GOT THIS";
                            break;
                        case 4:
                            gm.CheckPointInfo = "PERFECT";
                            break;
                        case 5:
                            gm.CheckPointInfo = "AMAZING";
                            break;
                    }
                }
            } 
            gm.Text_CheckPoint.enabled = true;
            gm.Text_CheckPoint.text = gm.CheckPointInfo;
            StartCoroutine(closeText(1f));   
        }
    } 
    private IEnumerator closeText(float time)
    {
        yield return new WaitForSeconds(time);
        gm.Text_CheckPoint.enabled = false;
    }
}
