using UnityEngine;
using System.Collections;

public class SelfDestroy : MonoBehaviour {

	void Start () {
		Destroy (this.gameObject, 10F);
	}
}
