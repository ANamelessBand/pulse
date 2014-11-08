using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour {

	public bool hasFlashlight;
	public float maxBatteryPower = 60;
	public float batteryPower;
	public float maxIntensity = 0.5F;
	public float minIntensity = 0.15F;

	private LightFlickering lightFlickering;

	// Use this for initialization
	void Start () {
		hasFlashlight = false;
		batteryPower = maxBatteryPower / 2;

		lightFlickering = gameObject.GetComponent<LightFlickering>();
	}
	
	// Update is called once per frame
	void Update () {
		if(hasFlashlight && batteryPower > 0 && !lightFlickering.is_flickering) {
			light.enabled = true;
			light.intensity = minIntensity + (batteryPower / maxBatteryPower) * (maxIntensity - minIntensity);
			batteryPower -= Time.deltaTime;
			lightFlickering.flickering_chance =
				lightFlickering.base_flickering_chance * (1 + (maxBatteryPower - batteryPower) / maxBatteryPower);  
			lightFlickering.flickering_duration =
				lightFlickering.base_flickering_duration * (1 + (maxBatteryPower - batteryPower) / maxBatteryPower);
		} else {
			light.enabled = false;
		}
	}
	
	public void refreshBatteries () {
			batteryPower = maxBatteryPower;
	}
}
