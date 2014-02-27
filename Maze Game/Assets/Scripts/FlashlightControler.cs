using UnityEngine;
using System.Collections;

public class FlashlightControler : MonoBehaviour {

	public float batteryLifeIncrement;
	public Light flashLight;
	public GUIText batteryLifeDisplay;
	public float maxBatteryLife;

	private float batteryLife;
	private float initialBrightness;
	private bool lightOn = true;
	
	void Start () {
		initialBrightness = flashLight.intensity;
		ExtendBatteryLife();
	}
	
	void Update () {
		if (lightOn) {
			batteryLife -= Time.deltaTime;
			if (batteryLife < 100)
				flashLight.intensity = initialBrightness / 2;
		} else {
			flashLight.intensity = 0f;
			lightOn = false;
		}
		batteryLifeDisplay.text = "Battery Life: " + Mathf.Round (batteryLife) + " seconds";
	}

	void ExtendBatteryLife () {

		if(batteryLife <= maxBatteryLife)
		batteryLife += batteryLifeIncrement;
		flashLight.intensity = initialBrightness;
	}
}
