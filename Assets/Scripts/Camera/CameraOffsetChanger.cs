using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOffsetChanger : MonoBehaviour
{
    [SerializeField]
    private string targetTag = "Player";

    [SerializeField]
    private Vector3 Offset = new Vector3(8, 5, -3);

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            Debug.Log("Camera Offset Change to " + Offset);
            FollowerCamera.Current().setOffset(Offset);
        }
    }
}
