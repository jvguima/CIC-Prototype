using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple script that makes the camera follow the position and rotation of the player
/// </summary>

public class CameraFollow : MonoBehaviour {

	[SerializeField] Transform target;

	Vector3 offset;

	void Start(){
		offset = this.transform.position - target.position;
	}
	
	// Update is called once per frame
	void Update () {

		this.transform.rotation = target.rotation;
		this.transform.position = target.position + offset;

	}
}
