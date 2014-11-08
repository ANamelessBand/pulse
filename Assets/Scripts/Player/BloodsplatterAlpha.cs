using UnityEngine;
using System.Collections;

public class BloodsplatterAlpha : MonoBehaviour {

	public GUITexture blood_splatter_texture;
	private Health health;

	void Start() {
		health = GameObject.FindWithTag("Player").GetComponent<Health>();
	}

	void Update () {
		var color = blood_splatter_texture.color;
		float alpha_level = health.MissingHealthRatio();
		if(alpha_level > 0.2) {
			alpha_level += 0.1F * Mathf.Sin(Time.time * 5);
		}
		color = new Color(color.r, color.g, color.b, alpha_level);
		blood_splatter_texture.color = color;
	}
}
