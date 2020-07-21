using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerCamera : MonoBehaviour
{
	private static FollowerCamera current = null;

	[SerializeField]
	private bool Active = true;

	[SerializeField]
	private Transform target;

	[SerializeField]
	[Range(0f, 0.5f)]
	private float smoothSpeed = 0.125f;

	[SerializeField]
	private Vector3 offset = new Vector3(8, 5, -3);

    private void Awake()
    {
        if(current)
        {
			Destroy(this);
			return;
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

    private void FixedUpdate()
	{
		if (!Active) return;
		Vector3 desiredPosition = target.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
		transform.position = smoothedPosition;

		transform.LookAt(target.position + Vector3.up);
	}

	public void setOffset(Vector3 vector)
    {
		offset = vector;

		//GetComponent<Cinemachine.CinemachineVirtualCamera>().
    }

	public static FollowerCamera Current()
    {
		return current;
    }
}
