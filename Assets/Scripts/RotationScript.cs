using UnityEngine;
using System.Collections;

//Simple script that rotates object continuesly
public class RotationScript : MonoBehaviour {
	public float _rate;
	float _currentRotation;
	void Update () {
		transform.rotation = Quaternion.Euler(new Vector3(0,0,_currentRotation));
		_currentRotation += Time.deltaTime*_rate;
		//reset
		if(_currentRotation > 360) {
			_currentRotation=0.0f;
		}
	}
}
