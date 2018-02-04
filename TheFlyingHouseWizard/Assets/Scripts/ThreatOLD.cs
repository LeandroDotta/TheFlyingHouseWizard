using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AutoMovement))]
public class ThreatOLD : MonoBehaviour 
{
	public ThreatStats stats;

	protected float health;

	private Animator anim;
	private Collider2D coll;
	private AutoMovement movement;
	

	private void Awake() 
	{
		anim = GetComponent<Animator>();
		coll = GetComponent<Collider2D>();
		movement = GetComponent<AutoMovement>();
	}

	private void Start() 
	{
		movement.speed = stats.speed;
		health = stats.health;	
	}

	private void OnTriggerEnter2D(Collider2D other) 
	{
		if(!coll.enabled)
			return;		

		if(other.CompareTag("Player"))
		{
			// Aplicar dano no jogador
			CharacterHealth playerHealth = other.GetComponentInParent<CharacterHealth>();
			// playerHealth.TakeDamage(stats.damage);
			
			Break();
		}
	}

	public void TakeDamage(float amount)
	{
		health -= amount;

		if(health <= 0)
		{
			health = 0;
			Break();
		}
	}

	public void Break()
	{
		if(anim != null)
			anim.SetTrigger("break");

		coll.enabled = false;
		//movement.enabled = false;

		StartCoroutine("BreakCoroutine");
	}

	private IEnumerator BreakCoroutine()
	{
		int length = 0;

		if(anim != null)
			length = anim.GetCurrentAnimatorClipInfo(0).Length;

		yield return new WaitForSeconds(length);

		Destroy(this.gameObject);
	}
}
