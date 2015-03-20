using UnityEngine;
using System.Collections;

public class LevelData : MonoBehaviour {


	/*
	 * The player will go from point A to point B  
	 * Obsticles will either push the player back, reset them or activate slippy mode
	 * Essentially the challenge is to get to the end whilst earning a high score
	 * When you are hit, you lose some of your score 
	 */

	public int _TargetDistance;
	public int _CurrentDistance;
	public int _CurrentScore;
	public GameObject[] _TargetCollectables;
	public GameObject[] _CurrentCollectables;
	public GameObject _player;
	public GameObject _progressBar;

	// Screens
	public GameObject _winScreen;
	public GameObject _loseScreen;
	public GameObject _mainScreen;

	public GameObject[] specCollectables; //Assign collectables to be spawned
	public int firstColSpawnMark;
	public int secondColSpawnMark;
	public int thirdColSpawnMark;
	public GameObject[] spawnPoint;
	public float dropSpeed;


	public bool _failed = false;

	// Match to store in playerData
	public int _level;

	void Start() {
		Assign ();
	}

	void Assign() {
		_CurrentCollectables = new GameObject[_TargetCollectables.Length];
		_player = GameObject.FindGameObjectWithTag ("PlayerData");
	}

	void Update() {
		// Each update we want to increment the distance
		if (_CurrentDistance < _TargetDistance && _failed == false) {
			IncreamentDistance ();
			CompareDistance ();
		}

		//Spawn collectables
		if(_CurrentDistance == firstColSpawnMark)
		{
			SpawnCollectable(0, Random.Range (0,spawnPoint.Length));
			Debug.Log ("I DID STUFF");
		}
		
		if(_CurrentDistance == secondColSpawnMark)
		{
			SpawnCollectable(1, Random.Range (0,spawnPoint.Length));
		}
		
		if(_CurrentDistance == thirdColSpawnMark)
		{
			SpawnCollectable(2, Random.Range (0,spawnPoint.Length));
		}
	}

	void Pause() {
		Time.timeScale = 0.0f;
	}

	void IncreamentDistance() {
		_CurrentDistance++;
		UpdateDistance ();
	}

	void DecrementDistance() {
		_CurrentDistance--;
		UpdateDistance ();
	}

	void IncrementDistanceAmount(int _amount) {
		_CurrentDistance += _amount;
		UpdateDistance ();
	}

	void DecrementDistanceAmount(int _amount) {
		_CurrentDistance -= _amount;
		UpdateDistance ();
	}

	// See if player has hit the end of the level
	void CompareDistance() {
		if (_CurrentDistance >= _TargetDistance) {
			// YOU WIN
			HideMain();
			ShowWin();
			WriteScore();
		}
		if (_CurrentDistance < 0) {
			ShowLose();
		}
	}

	void ShowWin() {
		Pause ();
		Debug.Log ("win");
		_winScreen.SetActive (true);
	}

	void ShowLose() {
		Pause ();
		_loseScreen.SetActive (true);
	}
	void HideMain() {
		_mainScreen.SetActive (false);
	}

	// See what collectables the player has collected 
	public void CompareCollectablesHighlight(GameObject _hit) {
		foreach (GameObject _collectable in _TargetCollectables) {
			if(_hit == _collectable) { 
				// Highlight the collectable here
				break;
			}
		}
	}

	// Update the progress on the slider
	void UpdateDistance() {
		// Calculate percentage
		float _percent = (_CurrentDistance*1.0f)/(_TargetDistance*1.0f); 
		_progressBar.GetComponent<UISlider> ().value = _percent;
	//	Debug.Log (_CurrentDistance);
	}

	// Record score in PlayerData
	void WriteScore() {
		_player.GetComponent<PlayerData>()._levelScores[_level] = _CurrentScore;
	}

	// Increament Score
	void IncreamentScore() {
		_CurrentScore++;
	}

	// Decrement Score
	void DecrementScore() {
		_CurrentScore--;
	}

	// Reset the scene (reload it)
	public void Reset() {
		_CurrentScore = 0;
		_CurrentDistance = 0;
	}

	private void SpawnCollectable (int sprite, int lane)
	{
		GameObject clone;
		clone = Instantiate (specCollectables [sprite], spawnPoint [lane].transform.position, Quaternion.identity) as GameObject;
		clone.GetComponent<ObjectMovementScript> ()._speed = dropSpeed;
	}

}
