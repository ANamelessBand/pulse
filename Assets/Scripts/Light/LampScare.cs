using UnityEngine;
using System.Collections;

public class LampScare : MonoBehaviour {
	private GameObject player;
	private LightController light_controller;

	public GameObject enemy;
	public bool is_active = false;
	public float delay_between_lights = 0.5F;
	public bool is_real = false;

	void Start () {
		player = GameObject.FindWithTag("Player");
		light_controller = this.gameObject.GetComponentInChildren<LightController>();
	}

	void Update () {
		if (!is_active && Input.GetKeyDown(KeyCode.T)) {
			is_active = true;
			Activate ();
		}
	}

	void Activate() {
		StartCoroutine("PlayScare");
	}

	IEnumerator PlayScare() {
		light_controller.TurnOff();
		yield return new WaitForSeconds(delay_between_lights);
		var enemy_object = (GameObject)
			Instantiate(enemy,
			            transform.position - new Vector3(0, 5, 0),
			            Quaternion.identity);
		enemy_object.transform.LookAt(player.transform.position);
		light_controller.TurnOn();
		yield return new WaitForSeconds(delay_between_lights);
		light_controller.TurnOff();
		Destroy(enemy_object);
		yield return new WaitForSeconds(delay_between_lights);
		light_controller.TurnOn();

		if(is_real) {
			light_controller.TurnOff();
			yield return new WaitForSeconds(delay_between_lights);
			enemy_object = (GameObject)
				Instantiate(enemy,
				            transform.position - new Vector3(0, 5, 0),
				            Quaternion.identity);
			enemy_object.transform.LookAt(player.transform.position);
			light_controller.TurnOn();
			yield return new WaitForSeconds(delay_between_lights);
			light_controller.TurnOff();
			yield return new WaitForSeconds(delay_between_lights);
			light_controller.TurnOn();
		}
	}
}
