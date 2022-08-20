using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CrouchHeadChecker : MonoBehaviour
{
    public UnityEvent onCantStandUp;
    public UnityEvent onCanStandUp;

    private void OnTriggerEnter(Collider other)
    {
        this.onCantStandUp?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        this.onCanStandUp?.Invoke();
    }
}
