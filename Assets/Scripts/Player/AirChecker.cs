using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AirChecker : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;

    private void Awake()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            playerMovement.AirCollision(other.gameObject);
        }
    }
}