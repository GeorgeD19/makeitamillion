using UnityEngine;
using System.Collections;

public class ObsticleScript : MonoBehaviour {


	public GameObject _LevelData;

	public GameObject _GameOver;

	void Start() {
		Time.timeScale = 1.0f;
		_LevelData = GameObject.FindGameObjectWithTag ("LevelData");
	}

	void OnCollisionEnter2D(Collision2D _collision) {
		switch (_collision.gameObject.tag) {
		case "obsticleLevel1":
			DestroyTarget (_collision.gameObject);
			// Game Over
			Time.timeScale = 0;
			_LevelData.GetComponent<LevelData>()._failed = true;
			_GameOver.SetActive(true);
			break;
		case "obsticleLevel2":
			DestroyTarget (_collision.gameObject);
			// Pushed Back - access LevelData
			break;
		case "obsticleLevel3":
			DestroyTarget (_collision.gameObject);
			// Slippy
			break;
		}
	}
	
	void DestroyTarget(GameObject _target) {
		Destroy (_target);
	}
}
