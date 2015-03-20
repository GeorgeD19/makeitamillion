using UnityEngine;
using System.Collections;

public class collectManager : MonoBehaviour {

	// The UI representations
	public GameObject[] _collectables;
	// To match up the collectables
	public GameObject[] _prefabs;

	public void ActivateCollectable(GameObject _found) {
		// Here we iterate through the array and if the names match then the colour tint will be 255,255,255,255
		for (int _x=0; _x<_prefabs.Length; _x++) {
			if(_found == _collectables[_x]) {
				Color _targetColor = new Color(255,255,255);
				_collectables[_x].GetComponent<UISprite>().color = _targetColor;
			}
		}
	}
}
