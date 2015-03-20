using UnityEngine;
using System.Collections;
//using System.Collections.Generic;

public class CustomerSpawnScript : MonoBehaviour {
	
	GameObject[] customers;
	public Transform[] targets;
	public int NoOfCustomers = 3;
	public GameObject customerTemplate;
	
	public float movementSpeed = 3f; 
	private int _targetWaypoint = 0; 
	private Transform _waypoints;
	
	
	// Use this for initialization
	void Start () 
	{
		customers = new GameObject[NoOfCustomers];
		AddCustomerToList();
		AddCustomerToList();
		AddCustomerToList();
	//	GetFrontOfQueueOrder().itemNeeded = true;
		_waypoints = GameObject.Find("Waypoints").transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		
	}
	
	// Fixed update
	void FixedUpdate()
	{
		moveToWaypoints();
	}
	
	// Function that handles the movement to the waypoints. I've kept this fully intact for example purposes as this is where I'm
	// probably going wrong in terms of trying to integrate it into the script. rigidbody2D needs to be assigned to the newCustomer
	// object that is found localy in the AddCustomerToList() function which is what I have done in the script I was working on earlier 
	// For example Rigidbody2D customer = newCustomer.AddComponent<Rigidbody2D>(); customer.mass = 1; customer.GravityScale = 0; customer.AngularDrag = 3
	//
	
	private void moveToWaypoints()
	{
		Transform targetWaypoint = _waypoints.GetChild(_targetWaypoint);
		Vector3 relative = targetWaypoint.position - transform.position;
		Vector3 movementNormal = Vector3.Normalize(relative);
		float distanceToWaypoint = relative.magnitude;
		
		if (distanceToWaypoint < 0.1)
		{
			if (_targetWaypoint + 1 < _waypoints.childCount)
			{
				// Set new waypoint as target
				_targetWaypoint++;
			}
		}
		else
		{
			// Walk towards waypoint
			rigidbody2D.AddForce(new Vector2(movementNormal.x, movementNormal.y) * movementSpeed);
		}
	}
	
	public bool AddCustomerToList()
	{
		bool added = false;
		if(customers[NoOfCustomers -1] == null)
		{
			GameObject newCustomer = Instantiate(customerTemplate) as GameObject;
			
			for(int i = 0; i < NoOfCustomers; i++)
			{
				if(customers[i] == null && !added)
				{
					customers[i] = newCustomer;
					customers[i].transform.position = targets[i].position;
					added = true;
					Debug.Log("Customer added");
					if(customers[0])
					{
						
					}
				}
			}
		}
		return added;
	}
	
	public bool IsQueueEmpty()
	{
		if(customers[0] == null)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
	//public CustomerNeedsScript GetFrontOfQueueOrder()
	//{
//		return customers[0].GetComponent<CustomerNeedsScript>();
//	}
	
	public bool RemoveCustomer(int pos)
	{
		if(customers[pos] != null)
		{
			GameObject delCustomer = customers[pos];
			customers[pos] = null;
			Destroy(delCustomer);
		}
		else
		{
			return false;
		}
		
		for(int i = pos; i < NoOfCustomers - pos; i++)
		{
			if(i + 1 < NoOfCustomers)
			{
				customers[i] = customers[i+1];
				if(customers[i] != null)
				{
					customers[i].transform.position = targets[i].position;
				}
				customers[i+1] = null;
			}
		}
		if(!IsQueueEmpty())
		{
		//	GetFrontOfQueueOrder().itemNeeded = true;
		}
		return true;
	}
}
