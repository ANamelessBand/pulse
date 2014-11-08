using UnityEngine;
using System.Collections;

public class HeartbeatSound : MonoBehaviour {

	private AudioClip heartbeat;
	// Use this for initialization
	void Start () {
		heartbeat = Resources.Load ("Sounds/single_heartbeat") as AudioClip; 
		audio.clip = heartbeat;
	}
	
	// Update is called once per frame
	void Update () {
		if (!audio.isPlaying) {
			audio.Play ();
		}
	}
}
