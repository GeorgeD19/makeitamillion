using UnityEngine;
using System.Collections;

public class WallDetectorScript : MonoBehaviour {

	
	void OnCollisionEnter2D(Collision2D _collision) {
		Debug.Log ("collision - Wall Script");
		switch (_collision.gameObject.tag) {
		case "leftWall":
			Destroy(this.gameObject);
			break;
		case "rightWall":
			Destroy(this.gameObject);	
			break;
		case "topWall":
			//Destroy(this.gameObject);
			break;
		case "bottomWall":
			Debug.Log ("collision - Bottom Wall");
			Destroy(this.gameObject);
			break;
		}
	}
}
