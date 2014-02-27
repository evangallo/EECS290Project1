using UnityEngine;
using System.Collections.Generic;

/**
 * A script that controls the gun and creates a controller for bullets.
 */

public class GunControl : MonoBehaviour {
	//The actual gun.
    public Transform gun, prefab;
	//Determines which weapon is being used.
    public int weapon;
	//A queue for bullets.
    private Queue<ShotController> q;

	/**
	 * Creates a queue for shots that are fired.
	 */
	void Start () {
	    q = new Queue<ShotController>();
	}
	
	/**
	 * Updates whenever shots are fired.
	 */
	void Update () {
        Transform shot = (Transform)Instantiate(prefab, gun.position, Quaternion.identity);
        shot.rigidbody.AddForce(0f,0f,1f);
        
	}
}