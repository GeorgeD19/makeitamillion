using UnityEngine;
using System.Collections;

public class MarysMealsCallOnline : MonoBehaviour {

	public string _feedNumber;
	public string _url;
	WWW www;
	// Use this for initialization
	void Start () {
		www = new WWW(_url);
		StartCoroutine(CallBack());
	}
	
	IEnumerator CallBack() {
		yield return www;

		// check for errors
		if (www.error == null)
		{
			Debug.Log("WWW Ok!: " + www.data);
			_feedNumber =  "Now Feeding " + www.data.ToString();
		} else {
			_feedNumber = "Error Calling Site";
			Debug.Log("WWW Error: "+ www.error);
		}    
		Render();
	}

	void Render() {
		GetComponent<UILabel> ().text = _feedNumber;
	}
}
