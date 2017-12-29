using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissilePlaceholder : MonoBehaviour {

	public GameObject proj;

	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetMouseButtonDown (0)) {
			GameObject obj = GameObject.Instantiate (proj, this.transform.position, this.transform.rotation);
			Projectile p = obj.GetComponent<Projectile> ();
			p.Fire (this.transform.up, this.gameObject.layer);
		}	
	}
}
