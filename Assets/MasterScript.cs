using UnityEngine;
using System.Collections;

public class MasterScript : MonoBehaviour {

	int _bestScore = 0;
	int _gamesPlayed = 0;
	int _candy = 0;
	int _counterScore = 0;

	public float _mainmenualpha = 1.0f;
	public float _MainMenuCurrentAlpha = 1.0f;
	public float _fadeRate = 2.0f;
	
	GameObject _bestScoreText;
	GameObject _gamesPlayedText;
	GameObject _candyText;
	GameObject _MainMenu;
	public GameObject _counter;

	void Start() {
		_bestScoreText = GameObject.FindGameObjectWithTag ("bestscoretext");
		_gamesPlayedText = GameObject.FindGameObjectWithTag ("gamesplayedtext");
		_candyText = GameObject.FindGameObjectWithTag ("candytext");
		_MainMenu = GameObject.FindGameObjectWithTag ("mainmenu");
		ResetCounter ();
		UpdateStats ();
	}

	public void ToggleInterface() {
		if (_mainmenualpha == 1.0f) { _mainmenualpha = 0.0f; } else { _mainmenualpha = 1.0f; }
	}

	void Update() {
		if (_MainMenuCurrentAlpha > _mainmenualpha) {
			_MainMenuCurrentAlpha -= Time.deltaTime * _fadeRate;
			RenderGUI();
		}
		if(_MainMenuCurrentAlpha <  _mainmenualpha) {
			_MainMenuCurrentAlpha += Time.deltaTime * _fadeRate;
			RenderGUI();
		}
	}

	public void RenderGUI() {
		_MainMenu.GetComponent<UIPanel> ().alpha = _MainMenuCurrentAlpha;
	}

	public void IncrementCounter() {
		_counterScore++;
		UpdateCounter ();
	}

	public void ResetCounter() {
		_counterScore = 0;
		UpdateCounter ();
	}

	public void UpdateCounter() {
		_counter.GetComponent<UILabel> ().text = "" + _counterScore;
	}

	public void UpdateBestScore(int _score) {
			if (_score > _bestScore) {
					_bestScore = _score;
				}
			_gamesPlayed ++;
		}

	public void UpdateStats() {
		_bestScoreText.GetComponent<UILabel>().text = "BEST SCORE: "+_bestScore;
		_gamesPlayedText.GetComponent<UILabel>().text = "GAMES PLAYED: "+_gamesPlayed;
		_candyText.GetComponent<UILabel>().text = ""+_candy;
	}
}
