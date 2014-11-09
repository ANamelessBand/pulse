using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour {

	public GameObject trigger_object;
	void OnTriggerEnter (Collider other) {
		Debug.Log (other.gameObject.name);
		trigger_object.SendMessage("ActivateScare", SendMessageOptions.DontRequireReceiver);
	}
}
