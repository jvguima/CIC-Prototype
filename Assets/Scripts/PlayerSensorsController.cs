using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSensorsController : MonoBehaviour {

	/// <summary>
	/// Reference to the camera. Camera Parameters
	/// </summary>
	public Camera _cam;//Componente de câmera 
	[SerializeField] int _minCamZoom=12;
	[SerializeField] int _maxCamZoom=24;
	[SerializeField] float _maxCameraDist = 7.5f;


	private Vector3 lastCamPos;  //Last Camera Position
	private Vector3 mouseRelativeToPlayer; //Position of the mouse relative to the Center

	private bool _isPanning;

	// Use this for initialization
	void Awake () {
		if (_cam == null)
			_cam = GetComponentInChildren<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		ManualZoom ();
		manualPan ();
	}


	void ManualZoom(){
		//ZOOM+
		if (Input.GetKey((KeyCode.LeftControl)) && Input.GetAxis ("Mouse ScrollWheel") < 0f){
			if (_cam.orthographicSize < _maxCamZoom) {
				_cam.orthographicSize++;
			}
		}
		//ZOOM-
		else if (Input.GetKey((KeyCode.LeftControl))  && Input.GetAxis ("Mouse ScrollWheel") > 0f) {
			if (_cam.orthographicSize > _minCamZoom) {
				_cam.orthographicSize--;
			}
		}
	}


	void manualPan(){

		///Limits the camera X and Y positions to a radius defined by the player position + _radius
		_cam.transform.position = new Vector3 (
			Mathf.Clamp(_cam.transform.position.x, this.transform.position.x - _maxCameraDist, this.transform.position.x + _maxCameraDist),
			Mathf.Clamp(_cam.transform.position.y, this.transform.position.y - _maxCameraDist, this.transform.position.y + _maxCameraDist),
			_cam.transform.position.z);

		//if (Input.GetButtonDown ("EnablePan")) {
		if (Input.GetKeyDown((KeyCode.LeftShift))) {
			_isPanning = true;
			lastCamPos = _cam.transform.position;
		}

		//if (Input.GetButton ("EnablePan")) {
		if (Input.GetKey((KeyCode.LeftShift))) {
			mouseRelativeToPlayer = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;

			Vector2 mouseDirection = mouseRelativeToPlayer.normalized;
			//Vector3 offset = _cam.transform.position - player.transform.position;

			_cam.transform.Translate (mouseDirection * _maxCameraDist * Time.deltaTime);
		}
		//if (Input.GetButtonUp ("EnablePan")) {
		if (Input.GetKeyUp((KeyCode.LeftShift))) {
			_isPanning = false;
			Vector2 backVector = lastCamPos - _cam.transform.position;
			_cam.transform.Translate (backVector.normalized * 15 * Time.deltaTime);
		}
	}
}
