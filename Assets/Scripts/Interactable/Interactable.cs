using UnityEngine;
using System.Collections;

public abstract class Interactable : MonoBehaviour {

	public RaycastHit hit;
	public float maxDistance;
	protected GameObject player;
	public virtual void Start() {
		player = GameObject.FindWithTag ("Player");
		maxDistance = 2.5f;
	}

	public virtual void Update () {
		if (Input.GetKey(KeyCode.E)) {
			Transform playerTransform = player.transform;
			Transform objTransfrom = this.gameObject.transform;
			Physics.Raycast(objTransfrom.collider.bounds.center,
			                playerTransform.collider.bounds.center - objTransfrom.collider.bounds.center,
			                out hit, maxDistance);
			if(hit.collider && hit.collider.gameObject.GetInstanceID() == player.GetInstanceID()) {
				this.Interact();
			}
		}
	}

	public abstract void Interact();

}