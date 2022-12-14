using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Puddle : MonoBehaviour
{
    [Header("Settings")]
	[SerializeField] private float currentDamageTimer;
	[SerializeField] private float maxDamageTimer;
	[SerializeField] private float step;
	[SerializeField] private int damagePerCycle;

	public UnityEvent<int> onDamaged;

	private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
			StartCoroutine(DamageTimer(other.gameObject));
        }
    }

	IEnumerator DamageTimer(GameObject player)
	{
		currentDamageTimer = 0;
		while (currentDamageTimer < maxDamageTimer)
		{
			currentDamageTimer += step;
			this.onDamaged?.Invoke(damagePerCycle);
			yield return new WaitForSeconds(step);
		}
		yield return null;
	}
}
