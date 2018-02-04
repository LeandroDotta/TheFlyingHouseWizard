using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AutoMovement))]
public class ProjectileThreat : Threat 
{
	public bool stopWhenHitPlayer;
	private Animator anim;

	[HideInInspector]
	public AutoMovement movement;

	protected override void Awake() 
	{
		base.Awake();

		anim = GetComponent<Animator>();
		movement = GetComponent<AutoMovement>();
	}

	protected override void Start()
	{
		base.Start();

		movement.speed = stats.speed;
	}

	public override void Die()
	{
		coll.enabled = false;
		anim.SetTrigger("break");

		StartCoroutine("DieCoroutine");
	}

	public override void HitPlayer()
	{
		coll.enabled = false;
		anim.SetTrigger("hit");

		movement.enabled = !stopWhenHitPlayer;
		StartCoroutine("DieCoroutine");
	}

	private IEnumerator DieCoroutine()
	{
		AnimatorStateInfo info;

		do
		{
			info =  anim.GetCurrentAnimatorStateInfo(0);

			yield return new WaitForEndOfFrame();
			
		}while(!(info.IsName("Hit") || info.IsName("Break")));

		yield return new WaitForSeconds(info.length);

		Destroy(this.gameObject);
	}

	private void OnTriggerEnter2D(Collider2D other) 
	{
		if(!coll.enabled)
			return;

		if(other.CompareTag("Player"))
		{
			DamagingObjectStats dmgStats = (DamagingObjectStats)stats;

			CharacterHealth playerHealth = other.GetComponentInParent<CharacterHealth>();
			playerHealth.TakeDamage(dmgStats.damage);

			HitPlayer();
		}
	}
}
