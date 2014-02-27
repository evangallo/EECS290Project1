using UnityEngine;
using System.Collections;

public class Battery : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

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
