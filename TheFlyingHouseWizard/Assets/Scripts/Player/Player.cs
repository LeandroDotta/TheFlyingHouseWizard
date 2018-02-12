using UnityEngine;

public class Player : MonoBehaviour 
{
	public PlayerStats stats;

	public HealthBar healthBar;

	private CharacterHealth health;

	private void Awake() 
	{
		health = GetComponent<CharacterHealth>();
		health.SetMaxHealth(stats.maxHealth, true);

		healthBar.maxHealth = stats.maxHealth;
	}

	private void OnEnable() 
	{
		health.OnDie += OnDie;
		health.OnHealthChange += OnHealthChange;
	}

	private void OnDisable() 
	{
		health.OnDie -= OnDie;
		health.OnHealthChange -= OnHealthChange;
	}

	private void OnDie()
	{
		GameManager.Instance.RestartScene();
	}

	private void OnHealthChange(float currentHealth)
	{
		healthBar.SetHealth(currentHealth);
	}
}
