using UnityEngine;
using System.Collections;

/**
 * Literally all this class does is govern what happens when a battery is touched.
 * It's nothing special.
 */
public class Battery : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/**
	 * When a battery is touchedby the player,
	 * extend the battery life and destroy the battery object.
	 * @param other The collider that touched the battery.
	 */
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
		{
			Debug.Log ("You have touched a battery!");
            other.BroadcastMessage("ExtendBatteryLife");
            Destroy(gameObject);
        }
    }
}
