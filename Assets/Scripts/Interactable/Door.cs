using UnityEngine;
using System.Collections;

public class Door : Interactable {

	public bool isOpen = false;
	public bool isMoving = false;
	public float angleToOpen = 90f;
	public float timeToActive;
	public float timeInactive = 1f;
	public float timesToRotate = 50f;
	public float timesRotated = 0f;

	public override void Update() {
		if (timeToActive > 0) {
			timeToActive -= Time.deltaTime;
			if (isMoving) {		
				var angle = (isOpen ? angleToOpen : -angleToOpen) / timesToRotate;
				this.gameObject.transform.Rotate (0, angle, 0);
				if(++timesRotated >= timesToRotate) {
					isMoving = false;
					timesRotated = 0;
					isOpen = !isOpen;
				}
			}
		}
		base.Update ();
	}
	public override void Interact ()
	{
		if (timeToActive <= 0) {
			timeToActive = timeInactive;
			isMoving = true;
		}
	}
}
