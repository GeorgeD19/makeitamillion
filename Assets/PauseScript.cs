using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

	public void Pause() {
		Time.timeScale = 0.0f;
	}

	public void Resume() {
		Time.timeScale = 1.0f;
	}
}
