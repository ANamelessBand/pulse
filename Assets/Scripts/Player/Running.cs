﻿using UnityEngine;
using System.Collections;

public class Running : MonoBehaviour 
{
	public float walk_speed = 3f;
	public float run_speed = 4.5f;

	public float peak_walk_speed = 3.2f;
	public float peak_run_speed = 5f;
	
	public float current_walk_speed;
	public float current_run_speed;
	
	public float max_stamina = 5;
	public float min_stamina = 2;
	public float current_stamina;

	private bool is_running = false;
	private CharacterMotor character_motor;
	
	private AudioClip footstep_walk;
	private AudioClip footstep_run;

	void Start () 
	{
		is_running = false;
		character_motor = this.gameObject.GetComponent<CharacterMotor>();

		footstep_walk = Resources.Load ("Sounds/wood_walk_2") as AudioClip;
		footstep_run = Resources.Load ("Sounds/wood_walk_2_run") as AudioClip;

		audio.clip = footstep_walk;

		this.current_stamina = this.max_stamina;

		current_run_speed = run_speed;
		current_walk_speed = walk_speed;
	}

	void FixedUpdate ()
	{
		UpdateSpeed ();
		float speed = current_walk_speed;
		
		if (Input.GetKey(KeyCode.LeftShift) &&
		    character_motor.grounded &&
		    (current_stamina >= min_stamina ||
		    (is_running && current_stamina > 0)))
		{
			if (!is_running) {
				toggleRunning();
			}
			is_running = true;
			speed = current_run_speed;
			current_stamina -= Time.deltaTime; 
		} else {
			if (is_running) {
				toggleWalking();
			}
			is_running = false;
			current_stamina += Time.deltaTime;
			if (current_stamina > max_stamina) {
				current_stamina = max_stamina;
			}
		}

		character_motor.movement.maxForwardSpeed = speed;
	}

	void toggleRunning() {
		audio.clip = footstep_run;
		if (audio.isPlaying){
			audio.Stop ();
		}

		audio.Play ();
	}

	void toggleWalking() {
		audio.clip = footstep_walk;
		if (audio.isPlaying){
			audio.Stop ();
		}

		audio.Play ();
	}

	void Update() {
		if (isWalking () && !audio.isPlaying) {
			audio.Play ();
		} 

		if (!isMoving()) {
			audio.Stop();
		}
	
	}

	bool isWalking() {
		bool has_velocity = character_motor.movement.velocity.x != 0
			|| character_motor.movement.velocity.z  != 0;

		return has_velocity && !isRunning();
	}

	bool isMoving(){
		return isWalking() || isRunning();
	}

	bool isRunning() {
		return is_running;
	}

	private void UpdateSpeed()
	{
		if (gameObject.GetComponent<HeartMonitor> ().IsAtPeak ()) {
			current_run_speed = peak_run_speed;
			current_walk_speed = peak_walk_speed;
		} 
		else
		{
			current_run_speed = run_speed;
			current_walk_speed = walk_speed;
		}
	}
}