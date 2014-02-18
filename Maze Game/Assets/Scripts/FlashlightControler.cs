using UnityEngine;
using System.Collections;

public class FlashlightControler : MonoBehaviour {

	public float batteryLifeIncrement;
	public GameObject flashLight;
	public GUIText batteryLifeDisplay;

	private float batteryLife;
	
	void Start () {
		ExtendBatteryLife();
	}
	
	void Update () {
		if (batteryLife >= 0) {
			batteryLife -= Time.deltaTime;
		} else {
			flashLight.SetActive (false);
		}
		batteryLifeDisplay.text = "Battery Life: " + Mathf.Round (batteryLife);
	}

	void ExtendBatteryLife () {
		batteryLife += batteryLifeIncrement;
		flashLight.SetActive (true);
	}
}
