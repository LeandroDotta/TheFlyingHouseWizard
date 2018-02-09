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


	private bool attacking;
	private Vector2 mousePosition;

	private Animator anim;

	public Vector2 LookDirection { get{ return (mousePosition - (Vector2)transform.position).normalized; } }

	private void Awake() 
	{
		anim = GetComponent<Animator>();
	}

	private void Update() 
	{
		if(Input.GetMouseButton(0))
			mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		if(Input.GetMouseButtonUp(0) && playableArea.Contains(mousePosition) && !attacking)
		{
			Attack();
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
	}

	private void CastSpell()
	{
		GameObject spell = GameObject.Instantiate(spellPrefab);
		spell.transform.position = transform.position;
		spell.GetComponent<AutoMovement>().direction = LookDirection;

		spell.SetActive(true);
	}

	private void OnDrawGizmos() 
	{
		Gizmos.color = Color.yellow;

		Gizmos.DrawWireCube(playableArea.center, playableArea.size);
	}
}
