using UnityEngine;
using System.Collections;

public class Door : Interactable {

	public bool isOpen = false;
	public float angleToOpen = 70f;
	public float timeToActive;
	public float timeInactive = 1f;
	public override void Update() {
		if (timeToActive > 0) {
			timeToActive -= Time.deltaTime;
		}
		base.Update ();
	}
	public override void Interact ()
	{
		if (timeToActive <= 0) {
			var angle = isOpen ? angleToOpen : -angleToOpen;
			this.gameObject.transform.Rotate (0, angle, 0);
			timeToActive = timeInactive;
		}
	}
}
