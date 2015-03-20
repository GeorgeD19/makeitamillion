using UnityEngine;
using System.Collections;

/// <summary>
/// FingerFollow Script
/// Version: 1.0.0, Date: 05/03/2015, Blair Duncan
/// A script that handles the movement of the mug on the slider.
/// </summary>

public class FingerFollow : MonoBehaviour 
{
	public Transform mug;
	private Vector3 targetPosition;

	void Start () 
	{
		targetPosition = mug.position;
	}

	void Update()
	{
		mug.position = Vector3.Lerp(mug.position, targetPosition, Time.deltaTime * 500);
	}

	void OnTouchStay(Vector3 point) 
	{
		targetPosition = new Vector3(point.x, targetPosition.y, targetPosition.z);
	}
}
