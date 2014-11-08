using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	public float max_health = 100;
	public float current_health;

	public float dying_angle_step = 0.1F;
	public float dying_angle = 30;

	private bool is_dead;
	void Start () {
		is_dead = false;
		current_health = max_health;
	}

	public void Update() {
		if(is_dead) {
			return;
		}
		if(Input.GetKeyDown(KeyCode.K)) {
			TakeDamage(20);
		}
	}

	public void TakeDamage(float damage) {
		if(damage < 0) {
			return;
		}

		current_health -= damage;
		if(current_health <= 0) {
			Die();
		}
	}

	public void Die() {
		if(is_dead) {
			return;
		}

		GameObject.Find("GameOverText").GetComponent<GUIText>().enabled = true;
		is_dead = true;
		this.GetComponent<CharacterController>().enabled = false;
		this.GetComponent<CharacterMotor>().enabled = false;
		this.GetComponent<MouseLook>().enabled = false;
		this.transform.position += new Vector3(0, -1, 0);
		StartCoroutine("DyingCamera");
	}

	public IEnumerator DyingCamera() {
		var camera = GameObject.FindWithTag("MainCamera");
		camera.GetComponent<MouseLook>().enabled = false;
		camera.GetComponent<Blur>().enabled = true;
		for(float i = 0; i < dying_angle; i+= dying_angle_step) {
			camera.transform.rotation = new Quaternion(camera.transform.rotation.x - dying_angle_step,
		                                           camera.transform.rotation.y,
		                                           camera.transform.rotation.z,
		                                           camera.transform.rotation.w);
			yield return null;
		}
	}
}
