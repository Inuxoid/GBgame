using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VHS : MonoBehaviour
{
    [SerializeField] private UnityEvent onCoinTaken;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.onCoinTaken?.Invoke();
            Destroy(this.gameObject);
        }
    }
}
