using UnityEngine;
using System.Collections;

public class ReloadScene : MonoBehaviour {

	public void Reload() {
		Application.LoadLevel (Application.loadedLevel);
	}
}
