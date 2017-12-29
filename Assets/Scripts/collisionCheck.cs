using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionCheck : MonoBehaviour {

	public float id;
	public Health health;

	void OnCollisionEnter2D(Collision2D col){
		print (id);
		if (health != null) {
			health.takeDamage (1);
		}
	}
}
