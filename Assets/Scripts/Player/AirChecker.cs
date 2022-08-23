using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AirChecker : MonoBehaviour
{

    [Header("Components")]
    [SerializeField] PlayerMovement playerMovement;

    [Header("Settings")]
    [SerializeField] private float airDistance;

    private void Update()
    {
        RaycastHit hit;
        Ray airRayTopRight = new Ray(new Vector3(transform.position.x, transform.position.y + 0.4f), Vector3.right);
        Ray airRayBotRight = new Ray(new Vector3(transform.position.x, transform.position.y - 0.4f), Vector3.right);
        Ray airRayRight = new Ray(transform.position, Vector3.right);
        Ray airRayTopLeft = new Ray(new Vector3(transform.position.x, transform.position.y + 0.4f), Vector3.left);
        Ray airRayBotLeft = new Ray(new Vector3(transform.position.x, transform.position.y - 0.4f), Vector3.left);
        Ray airRayLeft = new Ray(transform.position, Vector3.left);
        if (Physics.Raycast(airRayRight, out hit, airDistance) ||
            Physics.Raycast(airRayLeft, out hit, airDistance) ||
            Physics.Raycast(airRayTopRight, out hit, airDistance) ||
            Physics.Raycast(airRayBotRight, out hit, airDistance) ||
            Physics.Raycast(airRayTopLeft, out hit, airDistance) ||
            Physics.Raycast(airRayBotLeft, out hit, airDistance)
            )
        {
            if (hit.collider.CompareTag("Ground"))
            {
                Debug.Log("Hoba");
                playerMovement.AirGroundCollision(hit.collider.gameObject);
            }
            else
            {
                if (Input.GetAxisRaw("Horizontal") * (hit.transform.position.x - this.transform.position.x) > 0)
                {
                    playerMovement.AirWallCollision(hit.collider.gameObject);
                }
            }
        }   
    }
}