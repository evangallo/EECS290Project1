using UnityEngine;
using System.Collections;

public class End : MonoBehaviour {

	// The level counter.
	public static float level = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			Application.LoadLevel (1);
			IncrementLevel ();
		}
	}

	void IncrementLevel(){
		level++;
	}

	public static float GetLevel(){
		return level;
	}
}
