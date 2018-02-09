using UnityEngine;

public class Player : MonoBehaviour 
{
	public PlayerStats stats;

	private CharacterHealth health;

	private void Awake() 
	{
		health = GetComponent<CharacterHealth>();
		health.SetMaxHealth(stats.maxHealth, true);
	}

	private void OnEnable() 
	{
		health.OnDie += OnDie;
	}

	private void OnDisable() 
	{
		health.OnDie -= OnDie;
	}

	private void OnDie()
	{
		GameManager.Instance.RestartScene();
	}
}
