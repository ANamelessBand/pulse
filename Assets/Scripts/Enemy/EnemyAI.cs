using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	public float attacking_distance = 3F;
	public float base_damage = 10F;
	public float reload = 3F;
	public float current_reload = 0F;
	public float ai_normal_speed = 5F;
	public float ai_enraged_speed = 10F;

	private GameObject player;
	private NavMeshAgent agent;
	private HeartMonitor heart_monitor;
	private Health health;

	void Start () {
		player = GameObject.FindWithTag("Player");
		heart_monitor = player.GetComponent<HeartMonitor>();
		health = player.GetComponent<Health>();
		agent = this.gameObject.GetComponent<NavMeshAgent>();
		current_reload = 0;
	}

	bool isLit() {
		return false;
	}

	void FixedUpdate () {
		var distance = Vector3.Distance(this.gameObject.transform.position, player.transform.position);
		if (distance >= attacking_distance) {
			agent.speed = Mathf.Min(isLit() ? this.ai_enraged_speed : this.ai_normal_speed, distance - attacking_distance + 5F);
			agent.SetDestination(player.transform.position);
		} else {
			agent.Stop();
		}
	}

	void Update () {
		var distance_to_player = Vector3.Distance(player.transform.position, gameObject.transform.position);
		if (!health.IsDead() && distance_to_player < attacking_distance && current_reload == 0) {
			DealDamage();
		} else {
			Reload();
		}
	}

	void Reload() {
		if(current_reload > 0) {
			current_reload -= Time.deltaTime;
			if (current_reload < 0) {
				current_reload = 0;
			}
		}
	}

	void DealDamage() {
		var damage = base_damage;
		damage *= 1 + health.CurrentHealthRatio();
		damage *= (float)heart_monitor.currentRate / (heart_monitor.baseLine + 20);
		health.TakeDamage(damage);
		current_reload = reload;
	}
}
