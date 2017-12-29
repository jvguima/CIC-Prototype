using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerRCSController : MonoBehaviour {

	[Header("Ship References")]
	/// <summary>
	/// The Player's _rb.
	/// </summary>
	public Rigidbody2D _rb;

	[SerializeField] float _strafeThrust = 0.5f;
	[SerializeField] float _yawThrust = 0.5f;
	[SerializeField] float _yawSlowDown = -0.05f;
	private bool _isStrafing = false;
	private bool _isYawing = false;

	void Awake(){
		if (_rb == null)
			_rb = GetComponent<Rigidbody2D> ();
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void FixedUpdate () {



		if (Input.GetKey (KeyCode.Space)) {
				_rb.AddForce(-5*_rb.velocity);
		}

		Strafe ();

		Yaw ();
	}


	void Strafe(){
		/// Horizontal > 0 - Move portward
		if (Input.GetAxis ("Horizontal") > 0) {
			_rb.AddForce(transform.right * _strafeThrust,ForceMode2D.Force);
		}

		/// Horizontal < 0 - Move starboardward
		if (Input.GetAxis ("Horizontal") < 0) {
			_rb.AddForce(-1 * transform.right * _strafeThrust,ForceMode2D.Force);
		}		
	}

	void Yaw(){
	
		if (Input.GetAxis("Yaw") < 0) {
			_rb.AddTorque (_yawThrust,ForceMode2D.Force);
			_isYawing = true;
		}

		if (Input.GetAxis("Yaw") > 0) {
			_rb.AddTorque (-_yawThrust,ForceMode2D.Force);
			_isYawing = true;
		}

		if (Input.GetAxis ("Yaw") == 0) {
			_rb.AddTorque(_rb.angularVelocity * _yawSlowDown, ForceMode2D.Force);
			_isYawing = false;
		}
	}

}
