using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
public class PlayerController : MonoBehaviour {
	
	public float movementSpeed = 5.0f;
	public float mouseSensitivity = 5.0f;
	public float jumpSpeed = 20.0f;
	public Camera PlayerCamera;
	public Camera WeaponCamera;
	public float playerHealth = 100.0f;
	
	float verticalRotation = 0;
	public float upDownRange = 60.0f;
	
	float verticalVelocity = 0;
	
	CharacterController characterController;
	
	// Use this for initialization
	void Start () {
		Screen.lockCursor = true;
		characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		//Death Check
		if (playerHealth <= 0.0f) {
			Application.LoadLevel (2);
		}
		// Rotation
		
		float rotLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
		transform.Rotate(0, rotLeftRight, 0);
		
		verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
		verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
		PlayerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
		
		// Movement
		
		float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
		float sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;
		
		verticalVelocity += Physics.gravity.y * Time.deltaTime;
		
		if( characterController.isGrounded && Input.GetButton("Jump") ) {
			verticalVelocity = jumpSpeed;
		}
		
		Vector3 speed = new Vector3( sideSpeed, verticalVelocity, forwardSpeed );
		
		speed = transform.rotation * speed;
		
		
		characterController.Move( speed * Time.deltaTime );

	}

	void OnCollisionEnter(Collider other){
		Debug.Log ("You're touching an object!");
		if (other.tag == "Monster") {
			Debug.Log ("You're touching a monster!");
			playerHealth -= 30.0f;
		} else {
			Debug.Log ("You touched a " + other.tag + ", which is NOT a Monster!");
		}
	}

}
