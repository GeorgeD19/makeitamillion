using UnityEngine;
using System.Collections;

public class StartScript : MonoBehaviour {

	public MasterScript _player;
	public CharacterScript _bird;
	public GameObject _counter;

	// Use this for initialization
	void Start () {
		GameObject _master = GameObject.FindGameObjectWithTag ("master");
		GameObject _birdObject = GameObject.FindGameObjectWithTag ("bird");
		_counter = GameObject.FindGameObjectWithTag ("counter");
		_counter.SetActive (false);
		_player = _master.GetComponent<MasterScript> ();
		_bird = _birdObject.GetComponent<CharacterScript> ();
	}

	public void StartGame() {
		_player.ToggleInterface ();
		_bird.EnableRigidBody2D ();
		_counter.SetActive (true);
		gameObject.SetActive(false);
	}

	public void EndGame() {
		_player.ToggleInterface ();
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (0)) {
			StartGame();
		}
	}
}
