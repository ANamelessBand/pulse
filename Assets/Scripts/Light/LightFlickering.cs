using UnityEngine;
using System.Collections;

public class LightFlickering : MonoBehaviour {
	public float flickering_chance = 0.001F;
	public float flickering_duration = 1.5F;
	
	
	void Update () {
		if(Random.Range(0.0F, 1.0F) < flickering_chance) {
			StartCoroutine("Flicker");
		}
	}
	
	private IEnumerator Flicker() {
		light.enabled = false;
		yield return new WaitForSeconds(flickering_duration);
		light.enabled = true;
	}
}
