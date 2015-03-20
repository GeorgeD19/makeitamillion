using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour {

	// Jump Mechanism
	public Vector2 _jumpForce = new Vector2(50, 300);

	// Initially static but once tapped we start flapping
	public bool _active = false;
	
	public bool dead = false;
	public bool godMode = false;

	Rigidbody2D _rigidBody;
	Vector2 _originalPosition;

	public Vector2 _velocity = new Vector2(-20, 0);
	public float range = 4;

	// Determines what way we are wanting to face
	public bool _target = true;

	// Other objects we will need to call
	public MasterScript _master;
	public GameObject _starter;
	public StartScript _starterScript;

	float _scale;
	Vector3 _right;
	Vector3 _left;
	
	void Start() {
		_rigidBody = gameObject.GetComponent<Rigidbody2D> ();
		_originalPosition = gameObject.transform.position;
		GameObject _masterObject = GameObject.FindGameObjectWithTag ("master");
		_starter = GameObject.FindGameObjectWithTag ("starter");
		_starterScript = _starter.GetComponent<StartScript> ();
		_master = _masterObject.GetComponent<MasterScript> ();
		DisableRigidBody2D ();
		_scale = transform.localScale.x;
		_right = new Vector3(-_scale,transform.localScale.y,transform.localScale.z);
		_left = new Vector3(_scale,transform.localScale.y,transform.localScale.z);
	}

	public void DisableRigidBody2D () {
		_rigidBody.isKinematic = true;
	}

	public void EnableRigidBody2D () {
		_rigidBody.isKinematic = false;
	}

	public void MoveLeft() {
		rigidbody2D.velocity = -_velocity;
		FaceLeft ();
	}

	public void MoveRight() {
		rigidbody2D.velocity = _velocity;
		FaceRight ();
	}
	
	public void FaceRight() {
		transform.localScale = _right;
	}

	public void FaceLeft() {
		transform.localScale = _left;
	}

	public void StayStill() {
		rigidbody2D.velocity = new Vector2(0.0f,0.0f);
	}

	public void Move() {
		if(_target) { MoveLeft(); } else { MoveRight(); }
	}
	
	public void Jump() {
		rigidbody2D.velocity = Vector2.zero;
		rigidbody2D.AddForce(_jumpForce);
	}

	public void ResetPosition() {
		gameObject.transform.position = _originalPosition;
	}

	public void EnableStarter() {
		_starter.SetActive (true);
	}

	public void ResetGame() {
		_master.ResetCounter ();
		StayStill();
		ResetPosition();
		DisableRigidBody2D();
		EnableStarter();
	}

	// Do Graphic & Input updates here
	void Update() {
			if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) ) {
				Jump();
				Move ();
			}
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		switch (col.gameObject.tag) {
		case "leftWall":
			_target = true;
			Move ();
			_master.IncrementCounter();
			break;
		case "rightWall":
			_target = false;
			Move ();
			_master.IncrementCounter();
			break;
		case "spike":
			_target = true;
			ResetGame();
			FaceRight();
			_starterScript.EndGame();
			break;
		}
	}
}
