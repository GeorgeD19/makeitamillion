using UnityEngine;
using System.Collections;

public class SplashScript : MonoBehaviour {

	// Try using the Unity 2D sprite object for these and make sure they fit on the screen
	public GameObject[] _splashObject;
	
	// The choice we will have to which screen we are choosing
	int _num;

	// Disables all splashScreens and leaves a random one remaining
	void Start () {
		_num = Random.Range(0,_splashObject.Length);
		DisableAll ();
		Enable (_splashObject [_num]);
	}
	
	void Enable(GameObject _target) {
		_target.SetActive (true);
	}

	void Disable(GameObject _target) {
		_target.SetActive (false);
	}

	void DisableAll() {
		foreach(GameObject _splash in _splashObject){
			Disable(_splash);
		} 
	}
}
