using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour 
{
	[System.Serializable]
	public struct DelayedThreat
	{
		public float delay;
		public GameObject threat;
	}

	public float startDelay;
	public DelayedThreat[] delayedThreats;
	public Wave nextWave;
	public bool finalWave;

	private Transform threats;

	private void Awake() 
	{
		threats = transform.Find("Threats");
		threats.gameObject.SetActive(false);
	}
	private void Start() 
	{
		Invoke("StartWave", startDelay);
	}

	private void StartWave()
	{
		SendMessageUpwards("WaveStarted", null, SendMessageOptions.DontRequireReceiver);

		threats.gameObject.SetActive(true);

		InvokeRepeating("CheckWaveEnded", 0, 1);
	}

	private void CheckWaveEnded()
	{
		int childCount = threats.childCount;

		if(childCount <= 0)
		{
			SendMessageUpwards("WaveEnded", finalWave, SendMessageOptions.DontRequireReceiver);

			if(!finalWave)
				StartNextWave();
		}
	}

	private void StartNextWave()
	{
		nextWave.gameObject.SetActive(true);

		Destroy(this.gameObject);
	}
}
