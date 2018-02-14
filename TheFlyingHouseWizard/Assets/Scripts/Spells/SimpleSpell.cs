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

		Vector3 scale = transform.localScale;
		if(Level == 0)
			scale.x = scale.y = 0.5f;
		else if(Level == 1)
			scale.x = scale.y = 0.7f;
		else if(Level == 2)
			scale.x = scale.y = 0.85f;
		else if(Level == 3)
			scale.x = scale.y = 1;

		transform.localScale = scale;
		Debug.Log("Spell Level: " + Level);
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
				switch(this.Level)
				{
					case 0:
					default:
						threat.TakeDamage(stats.damage);
						break;
					case 1:
						threat.TakeDamage(stats.damage * stats.level1Multiplier);
						break;
					case 2:
						threat.TakeDamage(stats.damage * stats.level2Multiplier);
						break;
					case 3:
						threat.TakeDamage(stats.damage * stats.level3Multiplier);
						break;
				}
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
