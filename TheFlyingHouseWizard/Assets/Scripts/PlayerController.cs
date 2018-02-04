using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	public GameObject spellPrefab;
	public Bounds playableArea;

	private void Update() 
	{
		if(Input.GetMouseButtonUp(0))
		{
			Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			if(playableArea.Contains(mousePos))
			{
				Vector2 direction =  mousePos - (Vector2)transform.position;

				GameObject spell = GameObject.Instantiate(spellPrefab);
				spell.transform.position = transform.position;
				spell.GetComponent<AutoMovement>().direction = direction;

				spell.SetActive(true);
			}
		}
	}

	private void OnDrawGizmos() 
	{
		Gizmos.color = Color.yellow;

		Gizmos.DrawWireCube(playableArea.center, playableArea.size);
	}
}
