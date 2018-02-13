using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour 
{
	private void WaveStarted()
	{
		Debug.Log("Wave Started");
	}

	private void WaveEnded(bool isFinal)
	{
		Debug.Log("Wave Ended");

		if(isFinal)
			GameManager.Instance.RestartScene();
	}
}
