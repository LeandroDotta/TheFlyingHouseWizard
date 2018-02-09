using UnityEngine;

public class CharacterHealth : MonoBehaviour 
{
	public float maxHealth;
	private float _health;
	public float Health { get{ return _health; } }

	public delegate void ReceiveDamageAction(float amount);
	public delegate void RestoreHealthAction(float amount);
	public delegate void HealthChangeAction(float currentHealth);
	public delegate void DieAction();
	public event ReceiveDamageAction OnReceiveDamage;	
	public event RestoreHealthAction OnRestoreHealth;
	public event HealthChangeAction OnHealthChange;
	public event DieAction OnDie;

	private void Awake() 
	{
		_health = maxHealth;
	}

	public void TakeDamage(float amount)
	{
		if(amount < 0)
			throw new System.Exception("You can't deal a negative damage value to the character health");
		
		if(OnReceiveDamage != null)
			OnReceiveDamage.Invoke(amount);

		_health -= amount;
		_health = Mathf.Clamp(_health, 0, maxHealth);

		if(OnHealthChange != null)
			OnHealthChange.Invoke(_health);

		if(_health == 0 && OnDie != null)
			OnDie.Invoke();	
	}

	public void Restore(float amount)
	{
		if(amount < 0)
			throw new System.Exception("You can't restore negative value to the character health");

		if(OnRestoreHealth != null)
			OnRestoreHealth.Invoke(amount);

		_health += amount;
		_health = Mathf.Clamp(_health, 0, maxHealth);

		if(OnHealthChange != null)
			OnHealthChange.Invoke(_health);
	}

	public void SetMaxHealth(float newMaxHealth, bool updateHealth = false)
	{
		maxHealth = newMaxHealth;

		if(updateHealth)
			_health = maxHealth;
	}
}
