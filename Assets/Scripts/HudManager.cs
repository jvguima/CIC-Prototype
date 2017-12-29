using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour {


	public static HudManager _instance;

	[Header("NAVIGATION ELEMENTS")]
	[SerializeField] Text _txtSpeed;
	[SerializeField] Slider _sliderYaw;

	void Awake(){
		if (_instance == null)
			_instance = this;
		else
			Destroy (this);
	}

	/// <summary>
	/// Updates the Text. Called by the Navigation Controller
	/// </summary>
	/// <param name="speed">Speed.</param>
	public void UpdateSpeed(float speed){
		_txtSpeed.text = "SPD: " + speed.ToString("0.#") + " m/s";
	}

	public void UpdateYaw(float value){
		_sliderYaw.value = value;
	}
}
