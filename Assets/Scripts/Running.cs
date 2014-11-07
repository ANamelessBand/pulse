using UnityEngine;
using System.Collections;

public class Running : MonoBehaviour 
{
	public float walk_speed = 7;
	public float run_speed = 20;
	
	public float max_stamina = 5;
	public float min_stamina = 2;
	public float current_stamina;

	private bool is_running = false;
	private CharacterMotor character_motor;

	void Start () 
	{
		is_running = false;
		character_motor = this.gameObject.GetComponent<CharacterMotor>();
		this.current_stamina = this.current_stamina;
	}

	void FixedUpdate ()
	{
		float speed = walk_speed;
		
		if (Input.GetKey(KeyCode.LeftShift) &&
		    character_motor.grounded &&
		    (current_stamina >= min_stamina ||
		    (is_running && current_stamina > 0)))
		{
			is_running = true;
			speed = run_speed;
			current_stamina -= Time.deltaTime; 
		} else {
			is_running = false;
			current_stamina += Time.deltaTime;
			if (current_stamina > max_stamina) {
				current_stamina = max_stamina;
			}
		}
		
		character_motor.movement.maxForwardSpeed = speed;
	}
}