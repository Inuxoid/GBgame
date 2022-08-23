using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CrouchHeadChecker : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private UnityEvent onCantStandUp;
    [SerializeField] private UnityEvent onCanStandUp;
    [Header("Settings")]
    [SerializeField] private float standDistance;

    private void Update()
    {
        RaycastHit hit;
        Ray crouchRayUp = new Ray(transform.position, Vector3.up);
        Ray crouchRayRightUp = new Ray(transform.position, new Vector3(0.3f, 1, 0));
        Ray crouchRayLeftUp = new Ray(transform.position, new Vector3(-0.3f, 1, 0));
        if ((Physics.Raycast(crouchRayUp, out hit, standDistance) || 
            Physics.Raycast(crouchRayRightUp, out hit, standDistance) || 
            Physics.Raycast(crouchRayLeftUp, out hit, standDistance)) && 
            hit.collider.CompareTag("Ground"))
        {
            this.onCantStandUp?.Invoke();
        }
        else
        {
            this.onCanStandUp?.Invoke();
        }

    }
}
