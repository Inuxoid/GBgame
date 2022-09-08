using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroundChecker : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private UnityEvent onGrounded;
    [SerializeField] private UnityEvent onAired;

    [Header("Settings")]
    [SerializeField] private float standDistance;

    private void Update()
    {
        RaycastHit hit;
        Ray crouchRayUp = new Ray(transform.position, Vector3.down);
        Ray crouchRayRightUp = new Ray(transform.position, new Vector3(0.1f, -standDistance, 0));
        Ray crouchRayLeftUp = new Ray(transform.position, new Vector3(-0.1f, -standDistance, 0));
        if ((Physics.Raycast(crouchRayUp, out hit, standDistance) ||
            Physics.Raycast(crouchRayRightUp, out hit, standDistance) ||
            Physics.Raycast(crouchRayLeftUp, out hit, standDistance)) &&
            hit.collider.CompareTag("Ground"))
        {
            this.onGrounded?.Invoke();
        }
        else
        {
            this.onAired?.Invoke();
        }

    }
}
