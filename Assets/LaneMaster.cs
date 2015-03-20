using UnityEngine;
using System.Collections;

public class LaneMaster : MonoBehaviour {

	// 1 - 3 lanes
	// 2.4 - 5 lanes

	public int _xLane = 1;
	public int _yLane = 1;

	void Start () {
		UpdateLanes ();
	}

	void UpdateLanes() {
		
		renderer.material.mainTextureScale = new Vector2 (_xLane, _yLane);
	}

}
