using UnityEngine;

public class Player : MonoBehaviour 
{
	private CharacterHealth health;

	private void Awake() 
	{
		health = GetComponent<CharacterHealth>();
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
