using UnityEngine;
using System.Collections;

public class ToggleStateScript : MonoBehaviour {

	private bool _state = true;

	public void Activate() {
		SetActive ();
		Parse ();
	}

	public void Deactive() {
		SetDeactive ();
		Parse ();
	}

	void SetActive() {
		_state = true;
	}

	void SetDeactive() {
		_state = false;
	}

	void Parse() {
		gameObject.SetActive (_state);
	}
}
