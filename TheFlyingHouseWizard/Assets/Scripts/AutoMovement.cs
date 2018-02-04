using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AutoMovement : MonoBehaviour 
{
	public float speed;
	public Vector2 direction;
	public bool lookToDirection = true;

	private Rigidbody2D rb2d;

	public void Awake()
	{
		rb2d = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate() 
	{
		rb2d.velocity = direction.normalized * speed;

		if(speed != 0 && lookToDirection)
		{
			transform.up = direction;
		}
	}

	private void OnDisable() 
	{
		rb2d.velocity = Vector2.zero;
	}
}
