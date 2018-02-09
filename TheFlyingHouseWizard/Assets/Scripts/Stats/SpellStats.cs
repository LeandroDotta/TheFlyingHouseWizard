using UnityEngine;

[CreateAssetMenu(menuName="Stat.../Spell", fileName="New Spell Stats")]
public class SpellStats : ScriptableObject 
{
    public float damage;
    public float speed;

    public float energyCost;

    [Header("Damage Multipliers")]
    public float level1Multiplier = 1.5f;
    public float level2Multiplier = 2;
    public float level3Multiplier = 3;
}