using UnityEngine;
using System.Collections;

public class HeartbeatSound : MonoBehaviour {

	private AudioClip heartbeat;
	private const float audio_beats_per_minute = 120.0F;
	private const float base_volume = 0.5F;

	private HeartMonitor heart_monitor;

	void Start() {
		heart_monitor = GameObject.FindWithTag("Player").GetComponent<HeartMonitor>();
	}

	void Update () {
		audio.pitch = heart_monitor.currentRate / audio_beats_per_minute;
		audio.volume = base_volume * ((float)heart_monitor.currentRate / heart_monitor.baseLine);
	}
}
