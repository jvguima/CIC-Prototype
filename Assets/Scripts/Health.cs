using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	[SerializeField] int _maxHp;
	[SerializeField] int _currentHp;

	[SerializeField] int _maxArmor;
	[SerializeField] int _currentArmor;

	/// <summary>
	/// The invicibility time in seconds.
	/// </summary>
	[SerializeField] float _invTime;

	private bool _isDead;
	private bool _canBeHurt;

	private void Awake(){
		_currentHp = _maxHp;
		_canBeHurt = true;
	}

	/// <summary>
	/// Takes the damage. Decreases currentHP or kills the gameobject
	/// </summary>
	/// <param name="damage">Amount of damage to be dealt.</param>
	public void takeDamage(int damage){

		if (_canBeHurt && !_isDead)
		{

			//Has Armor
			if (_currentArmor > 0) {

				//Damage overflow
				if (_currentArmor-damage < 0) {
					_currentHp = Mathf.Clamp ( _currentHp + (_currentArmor-damage), 0, _maxHp);
				}

				_currentArmor = Mathf.Clamp (_currentArmor - damage, 0, _maxArmor);

			} 
			//Has no armor
			else {
				_currentHp = Mathf.Clamp (_currentHp - damage, 0, _maxHp);
			}

			StartCoroutine(waitInvinciTime());
		}
		else
			return;


		if (_currentHp <= 0) {
			_isDead = true;
			this.die ();
		}
	}

	public float getMaxHealth(){
		return _maxHp;
	}

	public float getCurrentHealth(){
		return _currentHp;
	}
		
	/// <summary>
	/// Kills this gameobject.
	/// </summary>
	private void die (){
		Destroy(this.gameObject);
	}

	private IEnumerator waitInvinciTime() {
		_canBeHurt = false;
		yield return new WaitForSeconds(_invTime);
		_canBeHurt = true;
	}

}
