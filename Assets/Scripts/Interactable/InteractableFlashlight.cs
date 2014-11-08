using UnityEngine;
using System.Collections;

public class InteractableFlashlight : Interactable {

	public override void Interact() {
		GameObject.FindWithTag ("Flashlight").light.enabled = true;
		Destroy(this.gameObject);
	}
}
