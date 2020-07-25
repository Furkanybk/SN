﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum CheckPointType
{
    Checkpoint,
    Startpoint,
    Endpoint
};

public class CheckPointManager : MonoBehaviour
{
    [SerializeField]
    public CheckPointType type = CheckPointType.Checkpoint;

    private bool IsCheckpointPassed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (type == CheckPointType.Endpoint)
            {
                GameMenu.current.Complete();
                return;
            }

            GameManager.current.PlayerLastCheckPoint = transform.position;

            if (type != CheckPointType.Startpoint)
            {
                if (!IsCheckpointPassed)
                {
                    IsCheckpointPassed = true;
                    GameManager.current.ReachedCheckPoint++;
                }
                GameManager.current.Text_ReachedCheckPoint.text = "CheckPoint Saved : " + GameManager.current.ReachedCheckPoint;

                if (/*GameObject.FindGameObjectWithTag("Player")*/other.gameObject.transform.position.z != GameManager.current.PlayerLastCheckPoint.z)
                {
                    switch (GameManager.current.ReachedCheckPoint)
                    {
                        case 1:
                            GameManager.current.CheckPointInfo = "GOOD";
                            break;
                        case 2:
                            GameManager.current.CheckPointInfo = "NICE";
                            break;
                        case 3:
                            GameManager.current.CheckPointInfo = "YOU GOT THIS";
                            break;
                        case 4:
                            GameManager.current.CheckPointInfo = "PERFECT";
                            break;
                        case 5:
                            GameManager.current.CheckPointInfo = "AMAZING";
                            break;
                    }
                }
            }

            GameManager.current.Text_CheckPoint.enabled = true;
            GameManager.current.Text_CheckPoint.text = GameManager.current.CheckPointInfo;
            StartCoroutine(closeText(1f));
        }
    }
    private IEnumerator closeText(float time)
    {
        yield return new WaitForSeconds(time);
        GameManager.current.Text_CheckPoint.enabled = false;
    }
}
