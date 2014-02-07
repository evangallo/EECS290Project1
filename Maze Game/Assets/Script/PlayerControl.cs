using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public float speed;

	private float lateralMovement, forwardMovement;

	void Start () {

	}

	void Update () {
		lateralMovement = Input.GetAxis ("Horizontal");
		forwardMovement = Input.GetAxis ("Vertical");

		
	}

	void FixedUpdate () {
		transform.Translate (new Vector3 (Mathf.Cos(transform.localRotation.y) * speed * lateralMovement -
		                                  Mathf.Sin(transform.localRotation.y) * speed * forwardMovement, 
		                                  0f, 
		                                  Mathf.Sin(transform.localRotation.y) * speed * lateralMovement +
		                     			  Mathf.Cos(transform.localRotation.y) * speed * forwardMovement));

	}


}
