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
			Physics.Raycast(objTransfrom.position, playerTransform.position - objTransfrom.position, out hit, maxDistance);
			Debug.Log(hit.collider.gameObject.name);
			if(hit.collider && hit.collider.gameObject.GetInstanceID() == player.GetInstanceID()) {
				this.Interact();
			}
		}
	}

	public abstract void Interact();

}