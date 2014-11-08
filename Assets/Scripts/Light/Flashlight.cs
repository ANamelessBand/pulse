using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour {

	public bool hasFlashlight;
	public float maxBatteryPower = 40;
	public float batteryPower;
	public float maxIntensity = 0.5F;
	public float minIntensity = 0.15F;

	// Use this for initialization
	void Start () {
		hasFlashlight = false;
		batteryPower = maxBatteryPower / 2;
	}
	
	// Update is called once per frame
	void Update () {
		if(hasFlashlight && batteryPower > 0) {
			var light = this.gameObject.GetComponent<Light>();
			light.enabled = true;
			light.intensity = minIntensity + (batteryPower / maxBatteryPower) * (maxIntensity - minIntensity);
			batteryPower -= Time.deltaTime;
		}
	}
	
	public void refreshBatteries () {
			batteryPower = maxBatteryPower;
	}
}
