using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	private float lateralMovement, forwardMovement;

	void Start () {

	}

	void Update () {
		lateralMovement = Input.GetAxis ("Horizontal");
		forwardMovement = Input.GetAxis ("Vertical");
		
	}

	void FixedUpdate () {
		transform.Translate (new Vector3 (lateralMovement, 0f, forwardMovement));

	}
	

}
