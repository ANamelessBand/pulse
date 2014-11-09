using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using System.Collections.Generic;

public class HeartMonitor : MonoBehaviour 
{
	private int INITIAL_MEASURES_COUNT = 200;

	public int PEAK_OFFSET = 20;

	private bool initialized;

	private int initialMeasureCount;

	private int initialMeasureSum;

	private IHeartDataProvider dataProvider;

	public HeartState heartState;

	public int currentRate;

	public int baseLine;

	// Use this for initialization
	void Start () 
	{
		heartState = HeartState.NORMAL;
		initialized = false;
		initialMeasureCount = 0;
		initialMeasureSum = 0;
		baseLine = 70;
		dataProvider = new HeartDataProvider ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		currentRate = dataProvider.GetCurrentRate ();
		UpdateHeartData ();
	}

	public bool IsAtPeak() {
		return heartState == HeartState.PEAK;
	}

	private void UpdateHeartData() 
	{
		if (!initialized) 
		{
			initialMeasureSum += currentRate;
			initialMeasureCount++;
		}

		if (!initialized && initialMeasureCount >= INITIAL_MEASURES_COUNT) 
		{
			baseLine = initialMeasureSum / initialMeasureCount;

			initialized = true;
		}

		heartState = currentRate >= baseLine + PEAK_OFFSET && initialized ? HeartState.PEAK : HeartState.NORMAL;
	}
}
