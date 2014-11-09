using UnityEngine;
using System.Collections;

public class LedBracelet : MonoBehaviour {
	private AssemblyCSharp.FeedbackBracelet bracelet;
	private float max_Distance = 15F;

	public bool is_mocked = false;

	void Start () {
		try {
			bracelet = new AssemblyCSharp.FeedbackBracelet();
		} catch(System.Exception e) {
			Debug.Log(e.InnerException.Message);
		}
	}

	void Update () {
		if(bracelet == null)
			return;

		if(is_mocked) { 
			bracelet.setLeds(new bool[6] {true, true, true, true, true, true});
			return;
		}

		var enemies = GameObject.FindGameObjectsWithTag("Enemy");
		var bestDistance = float.MaxValue;
		foreach(var enemy in enemies) {
			var distance = Vector3.Distance(enemy.gameObject.transform.position, gameObject.transform.position);
			if(distance < bestDistance) {
				bestDistance = distance;
			}
		}

		bool[] leds = new bool[6];
		if(bestDistance <= max_Distance) {
			int j = 0;
			for(float i = 0; i <= max_Distance - bestDistance; i += max_Distance / 6, ++j) {
				leds[j] = true;
			}
		}

		bracelet.setLeds(leds);
	}
}
