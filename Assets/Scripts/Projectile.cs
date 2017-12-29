using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Dumb Projectile Script. 
/// </summary>

public class Projectile : MonoBehaviour {

	[SerializeField] float _thrust = 10.0f;
	[SerializeField] Vector2 _direction = new Vector2 (-1, 0);
	[SerializeField] int _damage = 10;
	[SerializeField] float _timeToLive = 10; //em Segundos

	private Rigidbody2D _rb;
	private SpriteRenderer _sr;


	void Awake(){
		_rb = (Rigidbody2D)GetComponent(typeof(Rigidbody2D));
		_sr = (SpriteRenderer)GetComponent(typeof(SpriteRenderer));
	}

	void Start(){
	}

	/// <summary>
	/// Fire the projecticle on the specified fireDirection 
	/// </summary>
	/// <param name="fireDirection">Fire direction.</param>
	/// <param name="_ownerLayer">Layer of the owner.</param>
	public void Fire (Vector2 fireDirection, int _ownerLayer){
		gameObject.layer = _ownerLayer;
		_direction = fireDirection;


		if (_direction.x < 1 && _sr!=null)
			_sr.flipX = true;
		
		_rb.AddForce (_direction.normalized * _thrust, ForceMode2D.Impulse);

		StartCoroutine (lifeCountdown ());
	}

	void Die(){
		Destroy (this.gameObject);
		Destroy (this);
	}

	void OnCollisionEnter2D(Collision2D col){
		//Entidade "danificável"
		Health entity = col.gameObject.GetComponent<Health> ();
		if (entity != null) {
			entity.takeDamage (_damage);
		}
		Die ();
	}


	IEnumerator lifeCountdown(){
		yield return new WaitForSeconds (_timeToLive);
		Die ();
	}
}
