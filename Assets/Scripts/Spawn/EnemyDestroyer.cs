using UnityEngine;
using System.Collections;

public class EnemyDestroyer : MonoBehaviour {
	
	void OnTriggerEnter(Collider enemy) {
		Destroy(enemy.gameObject);
	}
	
}