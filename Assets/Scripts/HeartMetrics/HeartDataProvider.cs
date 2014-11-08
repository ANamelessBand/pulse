using AssemblyCSharp;
using System.Collections.Generic;
using System;

public class HeartDataProvider : IHeartDataProvider
{
	private int NORMAL_RATE_MAXIMUM = 99;

	private int NORMAL_RATE_MINIMUM = 70;

	private int PEAK_RATE_MINIMUM = 100;

	private int PEAK_RATE_MAXIMUM = 120;

	private int SEQUENCE_STATE_LENGTH = 200;

	public int BASE_LINE = 80;

	private List<int> heartSequence;

	private int moment;

	private Random random;

	public HeartDataProvider()
	{
		heartSequence = new List<int> ();
		moment = 0;
		random = new Random ();

		int sequenceMask = random.Next ();
		sequenceMask &= ~(1);
		GenerateSequence (sequenceMask);
	}

	public int GetCurrentRate() {
		if (moment >= heartSequence.Count) 
		{
			moment = moment % heartSequence.Count;
		}

		return heartSequence [moment++];
	}

	private void GenerateSequence(int mask)
	{
		List<int> baseLineData = new List<int> ();
		for (int i = 0; i < SEQUENCE_STATE_LENGTH; ++i) 
		{
			baseLineData.Add(BASE_LINE);
		}
		heartSequence.AddRange (baseLineData);

		int pow = 1;
		for (int i = 0; i < 32; ++i) 
		{
			int peakState = mask & pow;
			bool isPeak = peakState == 0? false : true;
			heartSequence.AddRange(GenerateStateData(isPeak));
			pow = pow << 1;
		}
	}

	private List<int> GenerateStateData(bool isPeak)
	{
		int mod;
		int add;
		if (isPeak) 
		{
			mod = PEAK_RATE_MAXIMUM - PEAK_RATE_MINIMUM;
			add = PEAK_RATE_MINIMUM;
		} else
		{
			mod = NORMAL_RATE_MAXIMUM - NORMAL_RATE_MINIMUM;
			add = NORMAL_RATE_MINIMUM;
		}

		List<int> stateData = new List<int> ();
		for (int i = 0; i < SEQUENCE_STATE_LENGTH; ++i)
		{
			int entry = random.Next() % mod + add;
			stateData.Add(entry);
		}

		return stateData;
	}
}
