using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour {

	public bool hasFlashlight;
	public float maxBatteryPower = 40;
	public float batteryPower;
	public float maxIntensity = 0.5F;
	public float minIntensity = 0.15F;
	public bool flickerEnabled;

	// Use this for initialization
	void Start () {
		hasFlashlight = false;
		batteryPower = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(hasFlashlight && batteryPower > 0) {
			var light = this.gameObject.GetComponent<Light>();
			light.enabled = true;
			light.intensity = minIntensity + (batteryPower / maxBatteryPower) * (maxIntensity - minIntensity);
			batteryPower--;
		}
	}
	
	public void refreshBatteries () {
			batteryPower = maxBatteryPower;
	}
}
