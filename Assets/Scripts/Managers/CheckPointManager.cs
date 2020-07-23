using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheckPointManager : MonoBehaviour
{
    private GameManager gm; 
    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            gm.PlayerLastCheckPoint = transform.position;
            
            if(GameObject.FindGameObjectWithTag("Player").transform.position.z != gm.PlayerLastCheckPoint.z)
            {
                gm.CheckPointInfo = "YOU GOT THIS";
            }
            if (this.gameObject.layer != 12)
            {
                gm.CheckPointText.enabled = true;
                gm.CheckPointText.text = gm.CheckPointInfo;
                StartCoroutine(closeText(2f));  
            }
        }
    } 
    private IEnumerator closeText(float time)
    {
        yield return new WaitForSeconds(time);
        gm.CheckPointText.enabled = false;
    }
}
