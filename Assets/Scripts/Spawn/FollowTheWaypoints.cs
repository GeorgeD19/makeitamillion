// Comments to be added later. Code needs to be refactored. This is a basic implementation that has the beginnings of a FSM.
// The items that are spawned in follow the waypoints that have been set out.

using UnityEngine;

public class FollowTheWaypoints : MonoBehaviour 
{
	private int targetWaypoint = 0;
	private Transform waypoints;

	private enum State
	{
		Move,
	};
	
	private State customerState;
	
	void Start() 
	{
		customerState = State.Move;
		waypoints = GameObject.Find ("Waypoints").transform;
	}
	
	void Update()
	{
		states();
	}

	private void states()
	{
		switch(customerState) 
		{
		case State.Move:
			move();
			break;
		}
		
	}

	private void move()
	{
		if (targetWaypoint == 0) 
		{
			moveToWaypoints();
		} 

		else if (targetWaypoint == 1)
		{
			moveToWaypoints();
		}

		else if (targetWaypoint == 2)
		{
			moveToWaypoints();
		}

		else if (targetWaypoint == 3)
		{
			moveToWaypoints();
		}
	}
	
	private void moveToWaypoints()
	{
		Transform _targetWaypoint = waypoints.GetChild(targetWaypoint);
		Vector3 relative = _targetWaypoint.position - transform.position;
		Vector3 movementNormal = Vector3.Normalize(relative);
		float distanceToWaypoint = relative.magnitude;
		rigidbody2D.isKinematic = false;
		
		if (distanceToWaypoint < 0.1)
		{
			if (targetWaypoint + 1 < waypoints.childCount)
			{
				targetWaypoint++;
			}
			rigidbody2D.isKinematic = true;
		}
		else
		{
			rigidbody2D.AddForce(new Vector2(movementNormal.x, movementNormal.y) * (Time.deltaTime + 15));
		}
	}
}