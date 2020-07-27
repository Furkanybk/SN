using Cinemachine;
using System.Collections;
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

    [SerializeField]
    private bool IsCheckpointPassed = false;

    private void Start()
    {
        IsCheckpointPassed = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsCheckpointPassed) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (type)
            {
                case CheckPointType.Checkpoint:
                    if (!IsCheckpointPassed)
                    {
                        IsCheckpointPassed = true;
                        GameManager.current.ReachedCheckPoint++;
                    }
                    GameManager.current.Text_ReachedCheckPoint.text = "CheckPoint Saved : " + GameManager.current.ReachedCheckPoint;

                    if (collision.gameObject.transform.position.z != GameManager.current.PlayerLastCheckPoint.z)
                    {
                        switch (GameManager.current.ReachedCheckPoint)
                        {
                            case 1:
                                GameManager.current.CheckPointInfo = "NICE";
                                break;
                            case 2:
                                GameManager.current.CheckPointInfo = "GOOD";
                                break;
                            case 3:
                                GameManager.current.CheckPointInfo = "YOU GOT THIS";
                                break;
                            case 4:
                                GameManager.current.CheckPointInfo = "WOW";
                                break;
                            case 5:
                                GameManager.current.CheckPointInfo = "PERFECT";
                                break;
                            case 6:
                                GameManager.current.CheckPointInfo = "GOOD SLIDE";
                                break;
                            case 7:
                                GameManager.current.CheckPointInfo = "AGILE";
                                break;
                            case 8:
                                GameManager.current.CheckPointInfo = "WHAT A MOVEMENT";
                                break;
                            case 9:
                                GameManager.current.CheckPointInfo = "PERFECT SLIDE";
                                break;
                            case 10:
                                GameManager.current.CheckPointInfo = "ALMOST DONE";
                                break;
                            case 11:
                                GameManager.current.CheckPointInfo = "UNSTOPPABLE";
                                break;
                        }
                    }
                    break;
                case CheckPointType.Startpoint:
                    break;
                case CheckPointType.Endpoint:
                    if (!IsCheckpointPassed)
                    {
                        IsCheckpointPassed = true;
                    }
                    GameMenu.current.Complete();
                    break;
            }
            GameManager.current.Text_CheckPoint.enabled = true;
            GameManager.current.Text_CheckPoint.text = GameManager.current.CheckPointInfo;
            StartCoroutine(closeText(1.5f));
        }
    }

    private IEnumerator closeText(float time)
    {
        yield return new WaitForSeconds(time);
        GameManager.current.Text_CheckPoint.enabled = false;
    }
}
