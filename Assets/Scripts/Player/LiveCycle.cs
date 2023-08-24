using Dto;
using System.Collections;
using System.Collections.Generic;
using StateMachines.PlayerSM;
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
	[SerializeField] private UnityEvent<FloatNumberDto> onChangedMaxHp;
	[SerializeField] private Material mat;
	[SerializeField] private bool buyedSafe;
	[SerializeField] public GameObject deadScreen;
	[SerializeField] private PlayerSM sm;
	[SerializeField] public bool isDead;
	
	

    public float Hp
    {
        get => hp; set
        {
            if (value <= maxHp)
            {
				hp = value;
            }
            else
            {
				hp = maxHp;
            }
            if (value <= 0 && sm.CurrentState != sm.DeadState && !isDead)
            {
	            hp = 0;
	            FloatNumberDto dto = new FloatNumberDto() { value = Hp / maxHp };
	            onCounted?.Invoke(dto);
	            isDead = true;
				Death();
            }
        }
    }

    public void GetDamage(int amount)
	{
		if (!damaged || !buyedSafe)
		{
			Hp -= amount;
			damaged = true;
			FloatNumberDto dto = new FloatNumberDto() { value = (Hp / maxHp) * 100 };
			onCounted?.Invoke(dto);
		}
        else if (buyedSafe)
        {
			StartCoroutine(SafeTimer());
        }
	}

    public void SetMaxHP(int newMaxHP)
    {
	    maxHp = newMaxHP;
	    hp = newMaxHP;
	    FloatNumberDto dto = new FloatNumberDto() { value = newMaxHP};
	    onChangedMaxHp?.Invoke(dto);
    }

	public void Heal(int addHp)
	{
		Hp += addHp;
		FloatNumberDto dto = new FloatNumberDto() { value = (Hp / maxHp) * 100 };
		onCounted?.Invoke(dto);
	}
	
	public void Heal(float addHp)
	{
		Hp += addHp;
		FloatNumberDto dto = new FloatNumberDto() { value = (Hp / maxHp) * 100 };
		onCounted?.Invoke(dto);
	}

	public void Death()
	{
		deadScreen.SetActive(true);
		sm.ChangeState(sm.DeadState);
		//Debug.LogError(sm.CurrentState.name);
	}

	IEnumerator SafeTimer()
	{
		this.mat.color = Color.green;
		yield return new WaitForSeconds(safeTime);
		this.mat.color = Color.white;
		damaged = false;
	}
}
