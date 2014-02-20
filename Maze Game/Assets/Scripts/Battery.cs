﻿using UnityEngine;
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
		Debug.Log ("You have touched a battery!");
        if (other.tag == "Player")
        {
            other.BroadcastMessage("ExtendBatteryLife");
            Destroy(gameObject);
        }
    }
}
