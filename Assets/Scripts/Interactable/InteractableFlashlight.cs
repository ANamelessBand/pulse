using UnityEngine;
using System.Collections;

public class InteractableFlashlight : Interactable {

	public override void Interact() {
		GameObject.FindWithTag ("Flashlight").light.enabled = true;
		this.gameObject.GetComponent<AudioSource>().Play();
		Destroy(this.gameObject, 0.5f);
	}
}
