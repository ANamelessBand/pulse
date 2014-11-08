using UnityEngine;
using System.Collections;

public class Waggle : MonoBehaviour {
	public float MIN_X_VALUE = -20;

	public float MAX_X_VALUE = 20;

	public float waggleSpeed = 1f;

	public float rotationStep;

	private bool isRight;

	private bool changeDirection;

	// Use this for initialization
	void Start () {
		rotationStep = waggleSpeed;
		changeDirection = true;
		isRight = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		float rotation = gameObject.transform.rotation.eulerAngles.x;
		if (rotation > 180) {
			rotation = rotation - 360;
		}

		if ((rotation <= MIN_X_VALUE && !isRight) || (isRight && rotation >= MAX_X_VALUE)) {
			rotationStep *= -1;
			isRight = !isRight;
		}

		gameObject.transform.Rotate (rotationStep, 0, 0);
	}
}
