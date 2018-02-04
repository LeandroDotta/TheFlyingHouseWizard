using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSpawner : MonoBehaviour 
{

	public float delay;
	public GameObject[] prefabs;
	public Transform[] points;

	private void Start() 
	{
		StartCoroutine(SpawnCoroutine());
	}

	private IEnumerator SpawnCoroutine()
	{
		yield return new WaitForSeconds(1);

		while(true)
		{
			Spawn();

			yield return new WaitForSeconds(delay);
		}
	}

	private void Spawn()
	{
		GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
		Vector2 point = points[Random.Range(0, points.Length)].position;

		GameObject instance = GameObject.Instantiate(prefab);
		instance.transform.position = point;
		instance.SetActive(true);
	}
}
