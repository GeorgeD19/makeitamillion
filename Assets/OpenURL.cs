using UnityEngine;
using System.Collections;

public class OpenURL : MonoBehaviour {

	public string _url;

	void OnClick() {
		Application.OpenURL (_url);
	}
}
