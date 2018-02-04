using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AutoMovement))]
public class SimpleSpell : Spell 
{
	[HideInInspector]
	public AutoMovement movement;

	private void Awake() 
	{
		movement = GetComponent<AutoMovement>();
	}

	private void Start() {
		movement.speed = stats.speed;
	}

	private void OnTriggerEnter2D(Collider2D other) 
	{
		if(other.CompareTag("Threat"))
		{
			Threat threat = other.GetComponent<Threat>();

			if(threat.stats.oneHit)
			{
				threat.Die();
			}
			else
			{
				threat.TakeDamage(stats.damage);
			}

			Destroy(this.gameObject);
		}
	}
}
