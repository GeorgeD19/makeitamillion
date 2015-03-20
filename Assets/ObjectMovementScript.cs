﻿using UnityEngine;
using System.Collections;

public class ObjectMovementScript : MonoBehaviour {

	public float _speed = 0.1f;

	void Update () {
		Vector3 pos = transform.position;
    	pos.y -= _speed;
     	transform.position = pos; 
	}
}
