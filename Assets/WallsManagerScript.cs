using UnityEngine;
using System.Collections;

public class WallsManagerScript : MonoBehaviour {

	// Attach this script to the main camera

	// Reference the collisions we are going to adjust
	public BoxCollider2D _topWall;
	public BoxCollider2D _bottomWall;
	public BoxCollider2D _leftWall;
	public BoxCollider2D _rightWall;
	public GameObject _camera;

	void Start() {

		Assign ();

		_topWall.size = new Vector2 (_camera.camera.ScreenToWorldPoint (new Vector3 (Screen.width * 2f, 0f, 0f)).x, 1f);
		_topWall.center = new Vector2 (0f, _camera.camera.ScreenToWorldPoint (new Vector3 ( 0f, Screen.height, 0f)).y + 0.5f);
	
		_bottomWall.size = new Vector2 (_camera.camera.ScreenToWorldPoint (new Vector3 (Screen.width * 2, 0f, 0f)).x, 1f);
		_bottomWall.center = new Vector2 (0f, _camera.camera.ScreenToWorldPoint (new Vector3( 0f, 0f, 0f)).y - 0.5f);
		
		_leftWall.size = new Vector2(1f, _camera.camera.ScreenToWorldPoint(new Vector3(0f, Screen.height*2f, 0f)).y);;
		_leftWall.center = new Vector2(_camera.camera.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).x - 0.5f, 0f);
		
		_rightWall.size = new Vector2(1f, _camera.camera.ScreenToWorldPoint(new Vector3(0f, Screen.height*2f, 0f)).y);
		_rightWall.center = new Vector2(_camera.camera.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x + 0.5f, 0f);
	}

	void Assign() {
		_camera = GameObject.FindGameObjectWithTag ("MainCamera");
		_topWall = GameObject.FindGameObjectWithTag ("topWall").GetComponent<BoxCollider2D>();
		_bottomWall = GameObject.FindGameObjectWithTag ("bottomWall").GetComponent<BoxCollider2D>();
		_leftWall = GameObject.FindGameObjectWithTag ("leftWall").GetComponent<BoxCollider2D>();
		_rightWall = GameObject.FindGameObjectWithTag ("rightWall").GetComponent<BoxCollider2D>();
	}
}

