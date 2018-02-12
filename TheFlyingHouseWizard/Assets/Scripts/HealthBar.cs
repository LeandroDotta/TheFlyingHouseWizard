using System.Collections;
using UnityEngine;

public class HealthBar : MonoBehaviour 
{
	public float maxHealth;
	private float currentSize;
	private float maxSize;

	private void Start() 
	{
		maxSize = transform.localScale.x;
		currentSize = maxSize;
	}

	public void SetHealth(float health)
	{
		health = Mathf.Clamp(health, 0, maxHealth);

		currentSize = (maxSize * health) / maxHealth;

		StopCoroutine("ResizeBarCoroutine");
		StartCoroutine("ResizeBarCoroutine");
	}

	public IEnumerator ResizeBarCoroutine()
	{
		float duration = 0.3f;
		float timer = 0;

		Vector3 scale = transform.localScale;
		float startScaleX = scale.x;

		while(timer < 1)
		{
			yield return new WaitForEndOfFrame();
			
			timer += Time.deltaTime / duration;
			Debug.Log(timer);

			scale.x = Mathf.Lerp(startScaleX, currentSize, timer);
			transform.localScale = scale;
		}
	}
}
