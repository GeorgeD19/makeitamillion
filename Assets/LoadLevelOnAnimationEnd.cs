using UnityEngine;
using System.Collections;

public class LoadLevelOnAnimationEnd : MonoBehaviour {

	float _animLength;
	public string _level;

	void Start() {
		_animLength = animation.clip.length;
		StartCoroutine ("LoadLevel");
	}

	IEnumerator LoadLevel() {
		animation.Play();
		yield return new WaitForSeconds(_animLength);
		Application.LoadLevel(_level);
	}
}
