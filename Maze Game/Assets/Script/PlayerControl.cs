using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public float speed, rotationSpeed;
	public float tiltRange;

	private float lateralMovement, forwardMovement;
	private float aimHorizontal, aimVertical;

	void Start () {

	}

	void Update () {
		lateralMovement = Input.GetAxis ("Horizontal");
		forwardMovement = Input.GetAxis ("Vertical");
		aimHorizontal = Input.mousePosition.x;
		aimVertical = Input.mousePosition.y;

		if (aimHorizontal < (Screen.width / 3) ||
		    aimHorizontal > (Screen.width * 2 / 3)) {
			transform.Rotate(new Vector3 (0f, 1f, 0f), 
			                 (aimHorizontal - Screen.width/2)/Screen.width 
			                 * rotationSpeed * Time.deltaTime, Space.World);
		}

		if ((aimVertical < (Screen.height / 3) ||
		    aimVertical > (Screen.height * 2 / 3)) &&
		    (transform.rotation.x < tiltRange ||
		    transform.rotation.x > 360f - tiltRange)) {
			transform.Rotate(new Vector3 (-1f, 0f, 0f), 
			                 (aimVertical - Screen.height/2)/Screen.height 
			                 * rotationSpeed * Time.deltaTime);
		}

		
	}

	void FixedUpdate () {
		transform.Translate (new Vector3 (Mathf.Cos(transform.localRotation.y) * lateralMovement -
		                                  Mathf.Sin(transform.localRotation.y) * forwardMovement, 
		                                  0f, 
		                                  Mathf.Sin(transform.localRotation.y) * lateralMovement +
		                                  Mathf.Cos(transform.localRotation.y) * forwardMovement) * 
		                     			  Time.deltaTime, Space.World);


	}


}
