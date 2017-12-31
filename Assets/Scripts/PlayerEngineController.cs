using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerEngineController : MonoBehaviour {

	[Header("Ship References")]
	/// <summary>
	/// The Player's _rb.
	/// </summary>
	[SerializeField] Rigidbody2D _rb;

	/// <summary>
	/// The thrust of the Ship. In kN ?
	/// </summary>
	[SerializeField] float _thrust = 1f;


	void Awake(){
		if (_rb == null)
			_rb = GetComponent<Rigidbody2D> ();
	}
		
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void FixedUpdate () {

		//Vector2 shipDirection = new Vector2 (transform.up.x, transform.up.y);

		///<summary>
		/// Use Engines
		/// Vertical > 0 - Move foreward
		/// Vertical < 0 - Move Sternward
		/// </summary>
		if (Input.GetAxis ("Vertical") > 0) {
			_rb.AddForce(transform.up * _thrust,ForceMode2D.Force);
		}

		if (Input.GetAxis ("Vertical") < 0) {
			_rb.AddForce(-1 * transform.up * _thrust,ForceMode2D.Force);
		}
	}
}
