using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(LineRenderer))]

/// <summary>
/// Player navigation controller. Responsible for Speed, Torque, etc.
/// </summary>
public class PlayerNavigationController : MonoBehaviour {


	[Header("Ship References")]
	/// <summary>
	/// The Player's _rb.
	/// </summary>
	[SerializeField] Rigidbody2D _rb;
	[SerializeField] LineRenderer _speedLr;
	[SerializeField] LineRenderer _accelLr;

	/// <summary>
	/// The Radius of the space between the ship and the beginning of the linerenderer.
	/// </summary>
	[SerializeField] int _lineBegRadius = 10;
	/// <summary>
	/// The maximum length of the line.
	/// </summary>
	[SerializeField] int _maxLineLength = 10;


	private Vector3 lastSpeedVect = Vector3.zero;
	private float lastAngularVelocity = 0;

	Vector3 speedVect;

	// Use this for initialization
	void Awake () {
		if (_rb == null)
			_rb = GetComponent<Rigidbody2D> ();
		if(_speedLr == null)
			_speedLr = GetComponent<LineRenderer> ();
	}

	void Start(){
	}
	
	void Update () {
		//Updates velocity vector
		speedVect = new Vector3 (_rb.velocity.x, _rb.velocity.y, 0);

		speedUpdate ();
		accelerationUpdate ();
		angularVelocityUpdate ();

		lastSpeedVect = speedVect;
		lastAngularVelocity = _rb.angularVelocity;
	}


	void speedUpdate(){
		///Used to draw the beginning of the line in a radius around the player
		Vector3 baseOffset = transform.position + speedVect.normalized * _lineBegRadius;

		_speedLr.SetPosition (0, baseOffset);
		_speedLr.SetPosition (1, baseOffset + Vector3.ClampMagnitude (speedVect, _maxLineLength));

		if(speedVect != lastSpeedVect)
			HudManager._instance.UpdateSpeed (speedVect.magnitude);
	}
		
	void accelerationUpdate(){
		Vector3 deltaV = (speedVect - lastSpeedVect)*250;

		Vector3 baseOffset = transform.position + deltaV.normalized * _lineBegRadius;

		if (speedVect != lastSpeedVect && deltaV.magnitude != 0) {
			_accelLr.SetPosition (0, baseOffset);
			_accelLr.SetPosition (1, baseOffset + Vector3.ClampMagnitude (deltaV, _maxLineLength));
		} else {
			_accelLr.SetPosition (0, baseOffset);
			_accelLr.SetPosition (1, baseOffset);
		}
	}

	void angularVelocityUpdate(){
		if (Input.GetAxis("Yaw") < 0) {
			HudManager._instance.UpdateYaw (-1f);
		}
		if (Input.GetAxis("Yaw") > 0) {
			HudManager._instance.UpdateYaw (1f);

		}
		if (Input.GetAxis ("Yaw") == 0) {
			HudManager._instance.UpdateYaw (0.5f);
		}
	}

}
