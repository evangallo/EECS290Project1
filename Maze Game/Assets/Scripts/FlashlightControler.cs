using UnityEngine;
using System.Collections;

/**
 * A class to control the flashlight behavior.
 */
public class FlashlightControler : MonoBehaviour {

	//The amount to increment battery life by when a battery is touched.
	public float batteryLifeIncrement;

	//The actual light that our flashlight represents
	public Light flashLight;

	//Text to display on-screen
	public GUIText batteryLifeDisplay;

	//The absolute maximum battery life possible
	public float maxBatteryLife;

	//Current battery life
	private float batteryLife;

	//Brightness of the flashlight when the game starts
	private float initialBrightness;

	//A variable to tell us if the light is on or off.
	private bool lightOn = true;

	/**
	 * Initializes the flashlight.
	 */
	void Start () {
		initialBrightness = flashLight.intensity;
		ExtendBatteryLife();
	}

	/**
	 * Checks to ensure that we still have battery life.
	 * If we don't, turns the battery off.
	 * Also updates the text displayed on screen.
	 */
	void Update () {
		if (lightOn) {
			batteryLife -= Time.deltaTime * End.GetLevel ();
			if (batteryLife < 100)
				flashLight.intensity = initialBrightness / 2;
			else if (batteryLife <= 0){
				lightOn = false;
			}
		} else {
			flashLight.intensity = 0f;
		}
		batteryLifeDisplay.text = "Battery Life: " + Mathf.Round (batteryLife) + " seconds";
	}

	/**
	 * A simple function to extend the battery life
	 * of the flashlight, provided that the battery
	 * isn't already at its maximum.
	 */
	void ExtendBatteryLife () {

		if(batteryLife <= maxBatteryLife) {
			//
			if((batteryLife += batteryLifeIncrement) > maxBatteryLife){
				batteryLife = maxBatteryLife;
			}
			flashLight.intensity = initialBrightness;
		}
	}
}
