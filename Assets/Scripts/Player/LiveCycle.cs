using Dto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LiveCycle : MonoBehaviour
{
	

    [Header("Settings")]
	[SerializeField] private float maxHp;
	[SerializeField] private float hp;
	[SerializeField] private bool damaged;
	[SerializeField] private float safeTime;
	[SerializeField] private UnityEvent onDeath;
	[SerializeField] private UnityEvent<FloatNumberDto> onCounted;
	[SerializeField] private Material mat;

	public float Hp { get => hp; set => hp = value; }

    public void GetDamage(int amount)
	{
		if (!damaged)
		{
			Hp -= amount;
			damaged = true;
			FloatNumberDto dto = new FloatNumberDto() { value = this.Hp / this.maxHp };
			this.onCounted?.Invoke(dto);
		}
        else
        {
			StartCoroutine(SafeTimer());
		}
			
		Debug.Log($"Damaged {amount}");

		if (Hp <= 0)
        {
			Death();
        }
	}

	public void GetHeart()
	{
		Hp++;
	}

	public void Death()
	{
		//Debug.Log($"Dead");
        this.onDeath?.Invoke();
        Destroy(gameObject.GetComponent<Collider>());
    }

	IEnumerator SafeTimer()
	{
		this.mat.color = Color.green;
		yield return new WaitForSeconds(safeTime);
		this.mat.color = Color.white;
		damaged = false;
	}
}
