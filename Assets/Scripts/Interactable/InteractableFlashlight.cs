using UnityEngine;
using System.Collections;

public class InteractableFlashlight : Interactable {

	public override void Interact() {
		Debug.Log("asd");
		GameObject.FindWithTag ("Flashlight").light.enabled = true;
		Destroy(this.gameObject);
	}
}
