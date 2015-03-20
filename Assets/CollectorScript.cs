using UnityEngine;
using System.Collections;

public class CollectorScript : MonoBehaviour {

	public GameObject _LevelData;

	int _score;

	void Start() {
		_LevelData = GameObject.FindGameObjectWithTag ("LevelData");
	}

	void OnCollisionEnter2D(Collision2D _collision) {
		switch (_collision.gameObject.tag) {
		case "collectableLevel1":
			_score++;
			DestroyTarget (_collision.gameObject);
			break;
		case "collectableLevel2":
			_score += 3;
			DestroyTarget (_collision.gameObject);
			break;
		case "collectableLevel3":
			_score += 5;
			DestroyTarget (_collision.gameObject);
			break;
		case "collectableLevel4":
			_score += 7;
			DestroyTarget (_collision.gameObject);
			break;
		case "collectableLevel5":
			_score += 9;
			DestroyTarget (_collision.gameObject);
			break;
		case "collectableLevel6":
			_score += 10;
			DestroyTarget (_collision.gameObject);
			break;
		case "collectableLevel7":
			_score += 20;
			DestroyTarget (_collision.gameObject);
			break;
		case "collectableSpecial":

			DestroyTarget (_collision.gameObject);
			break;
		}
		SyncScore ();
	}

	void SyncScore() {
		_LevelData.GetComponent<LevelData> ()._CurrentScore = _score;
	}
	void DestroyTarget(GameObject _target) {
		Destroy (_target);
	}
}
