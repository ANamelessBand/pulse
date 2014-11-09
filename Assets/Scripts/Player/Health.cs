using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	public float max_health = 100;
	public float current_health;

	public float in_combat_max = 5;
	public float in_combat = 0;
	public float regen_rate = 0.01F;

	public float dying_angle_step = 0.1F;
	public float dying_angle = 30;

	private bool is_dead;
	public float CurrentHealth() {
		return current_health;
	}

	public float MissingHealth() {
		return max_health - current_health;
	}

	public float MaxHealth() {
		return max_health;
	}

	public float MissingHealthRatio() {
		return MissingHealth() / MaxHealth();
	}

	public float CurrentHealthRatio() {
		return CurrentHealth() / MaxHealth();
	}

	public bool IsDead() {
		return is_dead;
	}
	
	void Start () {
		is_dead = false;
		current_health = max_health;
	}

	public void Update() {
		if(is_dead) {
			return;
		}

		RegenHealth();
	}

	public void RegenHealth() {
		if(in_combat > 0) {
			in_combat -= Time.deltaTime;
		} else {
			if(current_health < max_health) {
				current_health += regen_rate;
			}
			
			if(current_health > max_health) {
				current_health = max_health;
			}
		}
	}

	public void TakeDamage(float damage) {
		if(damage <= 0) {
			return;
		}

		GameObject.Find ("Bite").GetComponent<AudioSource>().Play();
		in_combat = in_combat_max;
		current_health -= damage;
		if(current_health <= 0) {
			Die();
		}
	}

	public void Die() {
		if(is_dead) {
			return;
		}

		current_health = 0;
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
