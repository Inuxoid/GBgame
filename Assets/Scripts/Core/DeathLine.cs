using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeathLine : MonoBehaviour
{
    public UnityEvent onDeathLine;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.onDeathLine?.Invoke();
        }
    }
}
