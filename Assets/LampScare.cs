using UnityEngine;
using System.Collections;

public class LampScare : MonoBehaviour {
	private GameObject player;
	private Light light_component;

	public GameObject enemy;
	public bool is_active = false;
	public float delay_between_lights = 0.5F;

	void Start () {
		player = GameObject.FindWithTag("Player");
		light_component = this.gameObject.GetComponentInChildren<Light>();
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
		light_component.enabled = false;
		yield return new WaitForSeconds(delay_between_lights);
		var enemy_object = (GameObject)
			Instantiate(enemy,
			            transform.position - new Vector3(0, 5, 0),
			            Quaternion.identity);
		enemy_object.transform.LookAt(player.transform.position);
		light_component.enabled = true;
		yield return new WaitForSeconds(delay_between_lights);
		light_component.enabled = false;
		Destroy(enemy_object);
		yield return new WaitForSeconds(delay_between_lights);
		light_component.enabled = true;
	}
}
