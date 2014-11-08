using UnityEngine;
using System.Collections;

public class HeartbeatSound : MonoBehaviour {

	private AudioClip heartbeat;
	private const float audio_beats_per_minute = 120.0F;
	private const float base_volume = 0.5F;

	private HeartMonitor heart_monitor;
	private Health health;

	void Start() {
		heart_monitor = GameObject.FindWithTag("Player").GetComponent<HeartMonitor>();
		health = GameObject.FindWithTag("Player").GetComponent<Health>();
	}

	void Update () {
		audio.pitch = heart_monitor.currentRate / audio_beats_per_minute;
		var audio_modified = (1 - base_volume) * health.MissingHealthRatio();
		audio.volume = base_volume + audio_modified;
	}
}
