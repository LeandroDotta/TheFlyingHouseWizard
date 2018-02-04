using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AutoMovement))]
public abstract class Spell : MonoBehaviour 
{
	public SpellStats stats;
}
