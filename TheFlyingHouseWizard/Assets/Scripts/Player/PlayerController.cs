using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	public GameObject spellPrefab;
	public Bounds playableArea;

	[Header("Childs")]
	public Transform arm;
	public Transform head;

	[Header("VFX")]
	public ParticleSystem chargingEffect;

	private bool attacking;
	private int chargeLevel;
	private Vector2 mousePosition;

	private Animator anim;
	private Player player;

	public Vector2 LookDirection { get{ return (mousePosition - (Vector2)transform.position).normalized; } }

	private void Awake() 
	{
		anim = GetComponent<Animator>();
		player = GetComponentInParent<Player>();
	}

	private void Update() 
	{
		if(Input.GetMouseButtonDown(0))
		{
			StartCharging();
		}

		if(Input.GetMouseButton(0))
			mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		if(Input.GetMouseButtonUp(0))
		{
			if(playableArea.Contains(mousePosition) && !attacking)
				Attack();

			StopCharging();
		}
	}

	private void Attack()
	{
		attacking = true;
		arm.up = LookDirection;
		anim.SetTrigger("attack");
	}

	private void AttackEnd()
	{
		attacking = false;
		arm.up = Vector2.up;

		SetChargeLevel(0);
	}

	private void CastSpell()
	{
		GameObject spell = GameObject.Instantiate(spellPrefab);
		spell.transform.position = transform.position;
		spell.GetComponent<AutoMovement>().direction = LookDirection;
		spell.GetComponent<Spell>().Level = chargeLevel;

		spell.SetActive(true);
	}

	private void StartCharging()
	{
		chargingEffect.Play();

		StartCoroutine("ChargeSpellCoroutine");
	}

	private void StopCharging()
	{
		chargingEffect.Stop();

		StopCoroutine("ChargeSpellCoroutine");
	}

	private IEnumerator ChargeSpellCoroutine()
	{
		yield return new WaitForSeconds(player.stats.level1ChargeTime);

		SetChargeLevel(1);

		yield return new WaitForSeconds(player.stats.level2ChargeTime);

		SetChargeLevel(2);

		yield return new WaitForSeconds(player.stats.level3ChargeTime);

		SetChargeLevel(3);
	}

	private void SetChargeLevel(int level)
	{
		chargeLevel = level;

		var emission = chargingEffect.emission;
		switch(level)
		{
			case 0:
			default:
				emission.rateOverTime = 5;
				break;

			case 1:
				emission.rateOverTime = 10;
				break;

			case 2:
				emission.rateOverTime = 25;
				break;

			case 3:
				emission.rateOverTime = 50;
				break;
		}
	}

	private void OnDrawGizmos() 
	{
		Gizmos.color = Color.yellow;

		Gizmos.DrawWireCube(playableArea.center, playableArea.size);
	}
}
