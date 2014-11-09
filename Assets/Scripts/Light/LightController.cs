using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour {
	private LightFlickering light_flickering;
	private AudioSource circuit_component;
	public bool is_disabled = false;
	void Start () {
		light_flickering = this.gameObject.GetComponent<LightFlickering>();
		circuit_component = this.gameObject.GetComponent<AudioSource>();
	}
	
	void Update () {
		if (!is_disabled && (light_flickering == null || !light_flickering.is_flickering)) {
			this.gameObject.light.enabled = true;
		} else {
			this.gameObject.light.enabled = false;
		}
	}

	public void TurnOff() {
		is_disabled = true;
		if(circuit_component != null) {
			circuit_component.Play();
		}
	}

	public void TurnOn() {
		is_disabled = false;
	}
}
