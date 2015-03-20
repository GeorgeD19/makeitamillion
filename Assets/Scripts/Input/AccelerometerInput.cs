/// <summary>
/// Accelerometer Script
/// Version: 2.0, Date: 19/03/2015, Blair Duncan
/// A script that handles the in-built accelerometer that all mobile devices have
/// It now has functionality to switch between different control features
/// This is dependent on whether a collision with an obstacle occurs
/// </summary>

using UnityEngine;
using System.Collections;

public class AccelerometerInput : MonoBehaviour 
{
	public bool collisionDetected = false;			// 	Set the deteced collision to intiallly be false
	public float timer;								// 	Set up the timer, it's set to 10 in the inspector
	public SwipeMovement swipeMovement;				//	Create an instance of the SwipeMovement class
	public AccelerometerInput acccelerometerInput;	//	Create an instance of the AccelerometerInput class

	/*
 	*	Update function is needed to ensure the accelerometer features are enabled
 	*	Without the Update function the accelerometer won't work
 	*/

	void Update() 
	{
		if (collisionDetected == true) 													// If there is a collsion then...
		{
			transform.Translate (Input.acceleration.x, 0, 0);							// 	This is what makes the accelerometer function
			timer -= Time.deltaTime;													//	Decrement the timer

			if(timer <= 0.0f)															// 	If the timer is < or = to 0 then...							
			{
				swipeMovement = gameObject.GetComponent<SwipeMovement>();				// 	Get the SwipeMovement component
				acccelerometerInput = gameObject.GetComponent<AccelerometerInput>();	//	Get the AccelerometerInput component
				acccelerometerInput.enabled = false;									//	Disable accelerometer controls
				swipeMovement.enabled = true;											//	Enable the SwipeMovement controls
				timer = 10.0f;															//	Reset the timer back to 10
			}
		}
	}

	/*
 	*	Various case statements will deal with the different obstacles in each level
 	*	Code follows a similar pattern to that found in the CollectroScript however it will deal with obstacles rather than collectables
 	*	They currently deal with collectables to enable testing of the control system as currently there are no obstacles to test
 	*	When obstacles are included in the game then this code can easily be altered to accomodate
 	*	When the timer runs out the lanes can sometimes be slightly dodgy in terms of placement, a slight tweak is needed to make it smoother
 	*/

	void OnCollisionEnter2D(Collision2D collision) 
	{
		switch (collision.gameObject.tag) 
		{
			case "obsticleLevel2":	//	Case for first obstacle
			TurnOnAccelerometer();		// 	If there is a collision, turn on the accelerometer and disable the swipe movement
			break;
		}
	}	

	/*
 	*	Function to deal with switching between controls when a collison occurs
 	*	When a collision occurs the swipe movement controls are disabled
 	*	When a collison occurs the accelerometer controls are enabled
 	*/

	void TurnOnAccelerometer()
	{
		collisionDetected = true;												//	Set the collisionDetected variable to true
		swipeMovement = gameObject.GetComponent<SwipeMovement>();				//	Get the SwipeMovement component
		acccelerometerInput = gameObject.GetComponent<AccelerometerInput>();	//	Get the AccelerometerInput componennt
		swipeMovement.enabled = false;											//	Disable swipeMovement controls
		acccelerometerInput.enabled = true;										//	Enable accelerometer controls
	}
}