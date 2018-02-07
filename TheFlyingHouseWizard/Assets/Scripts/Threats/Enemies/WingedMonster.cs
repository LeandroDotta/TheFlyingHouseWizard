using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingedMonster : Threat 
{
    public float distanceToAttack;

    private bool attacking = false;
    private float startY;

    private Animator anim;
    private AutoMovement movement;

    protected override void Awake() 
	{
		base.Awake();

		anim = GetComponent<Animator>();
		movement = GetComponent<AutoMovement>();
        coll = GetComponent<Collider2D>();
	}

	protected override void Start()
	{
        base.Start();

        startY = transform.position.y;
		movement.speed = stats.speed;
	}

    private void Update() 
    {
        if(movement.enabled && !attacking)
        {
            float currentY = transform.position.y;
            if(currentY <= startY - distanceToAttack)
            {
                movement.enabled = false;
                anim.SetTrigger("attack");
            }
        }
    }

    private void Attack()
    {
        attacking = true;
        
        movement.speed = stats.speed*2;
        movement.enabled = true;
    }

    public override void Die()
    {
        anim.SetTrigger("die");

        coll.enabled = false;
        movement.enabled = false;
        
        Destroy(this.gameObject, 1);
    }

    public override void HitPlayer()
    {
        anim.SetTrigger("hit");

        coll.enabled = false;
        movement.enabled = false;

        Destroy(this.gameObject, 1);
    }

    private void OnDrawGizmos() 
    {
        Vector2 attackPoint = new Vector2(transform.position.x, transform.position.y - distanceToAttack);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, attackPoint);
        Gizmos.DrawSphere(attackPoint, 0.2f);
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
