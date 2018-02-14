using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour 
{
	public SpellStats stats;

	public int Level { get; set; }
}
