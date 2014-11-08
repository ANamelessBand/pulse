using UnityEngine;
using System.Collections;

public class JumpScare : MonoBehaviour {
	public GameObject enemy;
	private GameObject player;
	private GameObject main_camera;

	private Quaternion original_angel;
	private bool is_active = false;
	private bool looked_back = false;

	void Start () {
		player = GameObject.FindWithTag("Player");
		main_camera = GameObject.FindWithTag("MainCamera");
	}

	void Update () {
		if (looked_back && Quaternion.Angle(main_camera.transform.rotation, original_angel) < 30) {
			Instantiate(enemy,
			            player.transform.position + player.transform.forward * 3,
			            Quaternion.Inverse(original_angel));
			is_active = false;
			looked_back = false;
		}

		if (is_active && Quaternion.Angle(main_camera.transform.rotation, original_angel) > 150) {
			looked_back = true;
		}

		if (Input.GetKeyDown(KeyCode.R)) {
			Activate();
		}
	}

	void Activate() {
		is_active = true;
		original_angel = main_camera.transform.rotation;
	}
}
