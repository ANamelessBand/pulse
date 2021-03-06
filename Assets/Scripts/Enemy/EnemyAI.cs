﻿using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	public float attacking_distance = 3F;
	public float base_damage = 10F;
	public float reload = 3F;
	public float current_reload = 0F;
	public float ai_normal_speed = 2.7F;
	public float ai_enraged_speed = 4.6F;
	public float ai_enraged_distance = 15F;
	public float ai_enraged_angle = 150F;
	public float distance_to_engage = 30F;	

	private GameObject player;
	private GameObject flashlight;
	private NavMeshAgent agent;
	private HeartMonitor heart_monitor;
	private Health health;

	void Start () {
		player = GameObject.FindWithTag("Player");
		heart_monitor = player.GetComponent<HeartMonitor>();
		health = player.GetComponent<Health>();
		flashlight = GameObject.Find("Flashlight");
		agent = this.gameObject.GetComponent<NavMeshAgent>();
		current_reload = 0;
	}

	bool isLit() {
		if (!flashlight.light.enabled) {
			return false;
		}
		var distance = Vector3.Distance(this.gameObject.transform.position, player.transform.position);
		var angle = Quaternion.Angle(player.transform.rotation, gameObject.transform.rotation);
		if(distance < ai_enraged_distance && angle > ai_enraged_angle) {
			return true;
		}

		return false;
	}

	void FixedUpdate () {
		var distance = Vector3.Distance(this.gameObject.transform.position, player.transform.position);
		if (distance >= attacking_distance && distance < distance_to_engage) {
			agent.speed = Mathf.Min(isLit() ? this.ai_enraged_speed : this.ai_normal_speed, distance - attacking_distance + 5F);
			agent.SetDestination(player.transform.position);
		} else {
			agent.Stop();
		}
	}

	void Update () {
		RaycastHit hit;
		Physics.Raycast(gameObject.collider.bounds.center,
		                player.collider.bounds.center - gameObject.collider.bounds.center, 
		                out hit, attacking_distance);
		if (!health.IsDead() && hit.distance != 0 &&
		    hit.collider.GetInstanceID() == player.collider.GetInstanceID() &&
		    current_reload == 0) {
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
