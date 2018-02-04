using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Threat : MonoBehaviour 
{
	public ThreatStats stats;
	protected float health;

	protected Collider2D coll;

	protected virtual void Awake() 
	{
		coll = GetComponent<Collider2D>(); 
	}

	protected virtual void Start()
	{
		health = stats.health;
	}

	public void TakeDamage(float amount)
	{
		health -= amount;

		if(health <= 0)
		{
			health = 0;
			Die();
		}
	}

	public virtual void Die()
	{
		Destroy(this.gameObject);
	}

	public virtual void HitPlayer()
	{
		Destroy(this.gameObject);
	}
}
