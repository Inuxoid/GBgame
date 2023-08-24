using System;
using Dto;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class BossHell : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private UnityEvent<FloatNumberDto> onHpChanged;
    [Header("Settings")]
    [SerializeField] private UnityEvent onDamaged;
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private Transform pointCenter;
    [SerializeField] private Transform pointUpperCenter;
    [SerializeField] private float pointDistance;
    
    private enum HellState { Appearance, FirstStage, SecondStage, ThirdStage, FourthStage, HellVictory, PlayerVictory }
    [SerializeField] private HellState hellState;
    
    private enum HellFirstState { ToStartPoint, ToLastPoint }
    [SerializeField] private HellFirstState hellFirstState;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Launch()
    {
        hellState = HellState.Appearance;
    }
    
    public void Appearance()
    {
        if (Vector3.Distance(transform.position, pointCenter.position) > pointDistance)
        {
            rigidbody.velocity = Vector3.MoveTowards(transform.position, pointCenter.position, pointDistance);
        }
        else
        {
            hellState = HellState.FirstStage;
            hellFirstState = HellFirstState.ToStartPoint;
        }
    }

    public void FirstStage()
    {
        switch (hellFirstState)
        {
            case HellFirstState.ToStartPoint:
                if (Vector3.Distance(transform.position, pointA.position) > pointDistance)
                {
                    rigidbody.velocity = Vector3.MoveTowards(transform.position, pointCenter.position, pointDistance);
                }
                else
                {
                    hellFirstState = HellFirstState.ToLastPoint;
                }
                break;
            case HellFirstState.ToLastPoint:
                if (Vector3.Distance(transform.position, pointB.position) > pointDistance)
                {
                    rigidbody.velocity = Vector3.MoveTowards(transform.position, pointCenter.position, pointDistance);
                }
                else
                {
                    hellFirstState = HellFirstState.ToLastPoint;
                }
                
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void SecondStage()
    {
        
    }
    
    public void GetDamage(int damage)
    {
        animator.SetBool("isHited", true);
        currentHealth -= damage;
        FloatNumberDto dto = new FloatNumberDto() { value = currentHealth / maxHealth };
        onHpChanged?.Invoke(dto);
        onDamaged?.Invoke();
        if (currentHealth <= 0 && !animator.GetBool("isDead"))
        {
            animator.SetBool("isDead", true);
            Death();
        }
    }

    public void Death()
    {

    }
}
