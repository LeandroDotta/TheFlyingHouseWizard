using UnityEngine;

[CreateAssetMenu(menuName="Stat.../Player", fileName="New Player Stats")]
public class PlayerStats : ScriptableObject
{
	public float maxHealth;
	public float maxEnergy;

	[Header("Spell Charging Time")]
	public float level1ChargeTime = 1;
	public float level2ChargeTime = 1;
	public float level3ChargeTime = 2;
}
