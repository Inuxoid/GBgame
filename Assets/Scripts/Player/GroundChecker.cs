using System;
using System.Collections;
using System.Collections.Generic;
using StateMachines.PlayerSM;
using UnityEngine;
using UnityEngine.Events;

public class GroundChecker : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private UnityEvent onGrounded;
    [SerializeField] private UnityEvent onAired;
    [SerializeField] private PlayerSM sm;

    [Header("Settings")]
    [SerializeField] private float standDistance;
    public LayerMask groundLayer;

    private RaycastHit hit;
    private Ray crouchRayDown;
    private Ray crouchRayRightDown;
    private Ray crouchRayLeftDown;
    
    private void Update()
    {
        crouchRayDown = new Ray(transform.position, Vector3.down);
        crouchRayRightDown = new Ray(transform.position, new Vector3(0.1f, -standDistance, 0));
        crouchRayLeftDown = new Ray(transform.position, new Vector3(-0.1f, -standDistance, 0));
        bool isGrounded = Physics.Raycast(crouchRayDown, out hit, standDistance, groundLayer) 
                          || Physics.Raycast(crouchRayRightDown, out hit, standDistance, groundLayer) 
                          || Physics.Raycast(crouchRayLeftDown, out hit, standDistance, groundLayer);

        if (isGrounded != sm.isGrounded)
        {
            sm.isGrounded = isGrounded;

            if (isGrounded)
            {
                onGrounded?.Invoke();
            }
            else
            {
                onAired?.Invoke();
            }
        }
    }
}
