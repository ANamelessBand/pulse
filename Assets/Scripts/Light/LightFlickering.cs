using UnityEngine;
using System.Collections;

public class LightFlickering : MonoBehaviour {
	public float base_flickering_chance = 0.003F;
	public float base_flickering_duration = 0.05F;
	
	public float flickering_chance = 0.001F;
	public float flickering_duration = 1.5F;

	public bool is_flickering;

	void Start() {
		flickering_chance = base_flickering_chance;
		flickering_duration = base_flickering_duration;
	}
	
	void Update () {
		if(light.enabled && Random.Range(0.0F, 1.0F) < flickering_chance) {
			StartCoroutine("Flicker");
		}
	}
	
	private IEnumerator Flicker() {
		is_flickering = true;
		yield return new WaitForSeconds(flickering_duration);
		is_flickering = false;
	}
}
