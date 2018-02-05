using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AutoMovement))]
public class SimpleSpell : Spell 
{
	[HideInInspector]
	public AutoMovement movement;

	private Animator anim;
	private Collider2D coll;

	private void Awake() 
	{
		movement = GetComponent<AutoMovement>();
		anim = GetComponentInChildren<Animator>();
		coll = GetComponent<Collider2D>();
	}

	private void Start() 
	{
		movement.speed = stats.speed;
	}

	private void OnTriggerEnter2D(Collider2D other) 
	{
		if(!coll.enabled)
			return;

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

			Die();
		}
	}

	private void Die()
	{
		anim.SetTrigger("hit");
		coll.enabled = false;
		movement.enabled = false;
		Destroy(this.gameObject, 1);
	}
}
