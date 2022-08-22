using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AirChecker : MonoBehaviour
{

    [Header("Components")]
    [SerializeField] PlayerMovement playerMovement;

    [Header("Settings")]
    [SerializeField] private float standDistance;

    private void Update()
    {
        RaycastHit hit;
        Ray crouchRayRight = new Ray(transform.position, Vector3.right);
        Ray crouchRayLeft = new Ray(transform.position, Vector3.left);
        if ((Physics.Raycast(crouchRayRight, out hit, standDistance) ||
            Physics.Raycast(crouchRayLeft, out hit, standDistance)) &&
            hit.collider.CompareTag("Ground"))
        {
            playerMovement.AirCollision(hit.collider.gameObject);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Ground")) 
    //    {
    //        playerMovement.AirCollision(other.gameObject);
    //    }
    //}
}