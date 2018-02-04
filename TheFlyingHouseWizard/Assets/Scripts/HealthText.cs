using UnityEngine;
using UnityEngine.UI;

public class HealthText : MonoBehaviour 
{
	public Text text;
	public CharacterHealth health;

	private void OnEnable() 
	{
		health.OnHealthChange += SetText;
	}

	private void OnDisable() 
	{
		health.OnHealthChange -= SetText;
	}

	private void Start() 
	{
		SetText(health.Health);
	}

	private void SetText(float health)
	{
		text.text = string.Format("Health: {0:0}", health);
	}
}
