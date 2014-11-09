using UnityEngine;
using System.Collections;

public class HeartbeatSound : MonoBehaviour {
	public const float audio_size = 0.72F;
	private AudioClip heartbeat;
	private const float base_volume = 0.5F;

	private HeartMonitor heart_monitor;
	private Health health;

	void Start() {
		heart_monitor = GameObject.FindWithTag("Player").GetComponent<HeartMonitor>();
		health = GameObject.FindWithTag("Player").GetComponent<Health>();
	}

	void Update () {
		audio.pitch = heart_monitor.currentRate / (60.0F / audio_size);
		var audio_modified = (1 - base_volume) * health.MissingHealthRatio();
		audio.volume = base_volume + audio_modified;
	}
}
