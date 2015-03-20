using UnityEngine;
using System.Collections;

public class PlayAnimationReverse : MonoBehaviour {

	// The animation name
	string _name;

	void Start() {
		_name = animation.clip.name;
		animation[_name].speed = -1;
		animation[_name].time = animation[_name].length;
		animation.Play ();
	}

}
