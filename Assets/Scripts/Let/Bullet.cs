using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponentInParent<LiveCycle>()?.GetDamage(damage);
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Player") || other.CompareTag("Wall") || other.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
        }
        
    }
}
