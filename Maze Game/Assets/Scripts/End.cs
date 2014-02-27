using UnityEngine;
using System.Collections;

/**
 * The script for ending the level.
 */
public class End : MonoBehaviour {

	// The level counter.
	public static float level = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/**
	 * When the player comes in contact with the end of the level,
	 * increment level counter and reload the level.
	 * @param other The object that came into contact with the level end.
	 */
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			IncrementLevel ();
			Application.LoadLevel (1);
		}
	}

	/**
	 * A simple function to increment the level counter.
	 */
	void IncrementLevel(){
		level++;
	}

	/**
	 * A simple getter for the level counter.
	 * @return The current level.
	 */
	public static float GetLevel(){
		return level;
	}
}
