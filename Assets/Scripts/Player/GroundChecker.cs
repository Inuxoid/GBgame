using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroundChecker : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private UnityEvent onGrounded;
    [SerializeField] private UnityEvent onAired;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            this.onGrounded?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            this.onAired?.Invoke();
        }
    }
}
