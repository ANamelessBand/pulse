using UnityEngine;
using System.Collections;

public class FlashlightBatteries : Interactable {

	public override void Interact() {
		var flashlight = GameObject.FindWithTag ("Flashlight").GetComponent<Flashlight>();
		if (flashlight.hasFlashlight) {
			flashlight.refreshBatteries();
			this.gameObject.GetComponent<AudioSource>().Play();
			Destroy (this.gameObject);
		}
	}
}
