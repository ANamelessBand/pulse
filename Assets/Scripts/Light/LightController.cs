using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour {
	private LightFlickering light_flickering;
	public bool is_disabled = false;
	void Start () {
		light_flickering = this.gameObject.GetComponent<LightFlickering>();
	}
	
	void Update () {
		if (!is_disabled && !light_flickering.is_flickering) {
			this.gameObject.light.enabled = true;
		} else {
			this.gameObject.light.enabled = false;
		}
	}
}
