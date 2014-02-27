using UnityEngine;
using System.Collections;

public class FlashlightControler : MonoBehaviour {

	public float batteryLifeIncrement;
	public Light flashLight;
	public GUIText batteryLifeDisplay;

	private float batteryLife;
	private float initialBrightness;
	
	void Start () {
		initialBrightness = flashLight.intensity;
		ExtendBatteryLife();
	}
	
	void Update () {
		if (batteryLife >= 0) {
			batteryLife -= Time.deltaTime;
		} else {
			flashLight.intensity = 0f;
		}
		batteryLifeDisplay.text = "Battery Life: " + Mathf.Round (batteryLife);
	}

	void ExtendBatteryLife () {

		if (batteryLife >= 100) {
			batteryLife += batteryLifeIncrement;
			flashLight.intensity = initialBrightness;
		}
	}
}
