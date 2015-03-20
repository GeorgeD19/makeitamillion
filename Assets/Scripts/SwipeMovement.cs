using UnityEngine;
using System.Collections;

/// <summary>
/// Version: 1.0.0, Date: 2/13/2015, Antonio Rusev
/// A script that handles the cup's movement,
/// using swipes on a touch screen or the arrows
/// as inputs.
/// </summary>

public class SwipeMovement : MonoBehaviour 
{


	enum ActiveSkillMode
	{
		Melee,
		Ranged
	};
	
	public enum OrientationType
	{
		Portrait,
		Landscape
	};
	
	public float PlayerSpeed = 20.0f;
	public bool DesktopTestMode = true;
	public OrientationType Orientation;
	public bool ReverseOrientation;


	TouchRecorder touchRecorder = new TouchRecorder();
	Vector2 active_mousepos = Vector2.zero;



	//Public variables
	public GameObject cup;         //The cup
	public float translationSpeed; //The speed of the translation //Not in use atm
	public GameObject[] lanes;     //The lanes between which the cup will be moving
	public int startingLane;       //Starting lane
	
	//Private variables
	public int index;             //Check for current lane
	public bool moveRight;        //Check for right arrow press
	public bool moveLeft;         //Check for left arrow press
	public Vector3 target;      //Translation target for cup
	
	
	// Use this for initialization
	void Start () 
	{
		//Starting position set up
		cup.transform.position = new Vector3 (lanes [startingLane].transform.position.x, cup.transform.position.y, cup.transform.position.z);
		
		//Check if cup object is set 
		if (cup == null) 
		{
			//Debug.Log("Cup object not set");
		}
		
		//Check if all lanes are set properly
		for(int i = 0;i < lanes.Length;i++)
		{
			if(lanes[i] == null)
			{
				//Debug.Log("Lane number: " + i + " not set!");
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Check current lane
		for(int i=0;i < lanes.Length;i++)
		{
			if(cup.transform.position.x == lanes[i].transform.position.x)
				index = i;
		}
		
		//If right arrow is pressed
		if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			//Debug.Log ("Right");
			RightAction();
		}
		
		//else if left arrow is pressed
		else if(Input.GetKeyDown (KeyCode.LeftArrow))
		{
			//Debug.Log ("Left");
			LeftAction();
		}
		
		if (Input.GetMouseButton (0)) {
			active_mousepos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

			if (!touchRecorder.isNewTouch ()) {
				touchRecorder.setNewTouch (true);
				touchRecorder.addTouch(active_mousepos);
				Vector3 calculatedPoint = Vector3.zero;
				calculatedPoint = Camera.mainCamera.ScreenToWorldPoint(active_mousepos);
				//touchRecorder.getTrailRenderer().transform.position = new Vector3(calculatedPoint.x, .5f, calculatedPoint.z);
			}
		}

		if (Input.GetMouseButtonUp (0)) {
			touchRecorder.addTouch (active_mousepos);
			Vector3 calculatedPoint = Vector3.zero;
			calculatedPoint = Camera.mainCamera.ScreenToWorldPoint(active_mousepos);
			//touchRecorder.getTrailRenderer().transform.position = new Vector3(calculatedPoint.x, .5f, calculatedPoint.z);
			touchRecorder.determineShapeApproximation();

			// Determines the Direction of a particular swipe
			DirectionDetermination();
			
			touchRecorder.Reset();

		}


		DirectionDetermination ();

		//Check if target has been reached
		if(cup.transform.position.x == target.x)
		{
			moveRight = false;
			moveLeft = false;
			//Debug.Log ("Reached target");
		}
		
		//Movement block
		if(moveRight || moveLeft)
		{
			target.z = cup.transform.position.z;
			target.y = cup.transform.position.y;
			cup.transform.position = Vector3.MoveTowards(cup.transform.position, target, Time.deltaTime *  translationSpeed);
		}
	}

	void LeftAction() {
		moveLeft = true;
		if (index <= lanes.Length-3 && target.x == lanes[index+1].transform.position.x)
			target = lanes[index+2].transform.position;
		else if(index != lanes.Length-1 && !moveRight)
			target = lanes[index+1].transform.position;
		else if(index != lanes.Length-1 && moveRight && target.x != lanes[index].transform.position.x)
		{
			target = lanes[index].transform.position;
			moveRight = false;
		}
		else if(index != lanes.Length-1 && moveRight && target.x == lanes[index].transform.position.x)
		{
			target = lanes[index+1].transform.position;
			moveRight = false;
		}
		else if (target.x == lanes[index-2].transform.position.x)	
			target = lanes[index].transform.position;
		else 
			moveLeft = false;
	}

	void RightAction() {
		moveRight = true;
		if (index >= 2 && target.x == lanes[index-1].transform.position.x)
			target = lanes[index-2].transform.position;
		else if(index != 0 && !moveLeft)
			target = lanes[index-1].transform.position;
		else if(index != 0 && moveLeft && target.x != lanes[index].transform.position.x)
		{
			target = lanes[index].transform.position;
			moveLeft = false;
		}
		else if(index != 0 && moveLeft && target.x == lanes[index].transform.position.x)
		{
			target = lanes[index-1].transform.position;
			moveLeft = false;
		}
		else if(target.x == lanes[index+2].transform.position.x)
			target = lanes[index].transform.position;
		else 
			moveRight = false;
	}

	private void DirectionDetermination()
	{
		// Change these particular basis behaviors to your desired outcome within a particular case statement
		// NOTE: Within a Portrait Perspective (Android Build), the Directions are rotated 90 degrees. 
		// Meaning, the ShapeType.Right will now behave like the ShapeType.Up, ShapeType.Down will now behave
		// like ShapeType.Left.
		switch ((ShapeType)touchRecorder.getDeterminedShape)
		{
		case ShapeType.SinglePoint:
		//	Debug.Log("Single Point.");
			break;
			
		case ShapeType.DownLeft:
		//	Debug.Log("Down Left");
			LeftAction();
			break;
			
		case ShapeType.DownRight:
		//	Debug.Log("Down Right");
			RightAction();
			break;
			
		case ShapeType.UpRight:
		//	Debug.Log("Up Right");
			RightAction();
			break;
			
		case ShapeType.UpLeft:
		//	Debug.Log("Up Left");
			LeftAction();
			break;
			
		case ShapeType.Left:
		//	Debug.Log("Left");
			LeftAction();
			break;
			
		case ShapeType.Down:
		//	Debug.Log("Down");
			break;
			
		case ShapeType.Right:
		//	Debug.Log("Right");
			RightAction();
			break;
			
		case ShapeType.Up:
		//	Debug.Log("Up");
			break;
			
		default:
		//	Debug.Log("Undefined.");
			break;
		}
	}

}
