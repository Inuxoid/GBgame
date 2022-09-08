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
        RaycastHit hit = new(); 
        RaycastHit hit1 = new();
        RaycastHit hit2 = new();
        RaycastHit hit3 = new();
        RaycastHit hit4 = new();
        RaycastHit hit5 = new();


        Ray airRayTopRight = new Ray(new Vector3(transform.position.x, transform.position.y + 0.7f), Vector3.right);
        Ray airRayBotRight = new Ray(new Vector3(transform.position.x, transform.position.y - 0.5f), Vector3.right);
        Ray airRayRight = new Ray(transform.position, Vector3.right);

        Ray airRayTopLeft = new Ray(new Vector3(transform.position.x, transform.position.y + 0.7f), Vector3.left);
        Ray airRayBotLeft = new Ray(new Vector3(transform.position.x, transform.position.y - 0.5f), Vector3.left);
        Ray airRayLeft = new Ray(transform.position, Vector3.left);
        if (Physics.Raycast(airRayRight, out hit, airDistance) ||
            Physics.Raycast(airRayLeft, out hit1, airDistance) ||
            Physics.Raycast(airRayTopRight, out hit2, airDistance) ||
            Physics.Raycast(airRayBotRight, out hit3, airDistance) ||
            Physics.Raycast(airRayTopLeft, out hit4, airDistance) ||
            Physics.Raycast(airRayBotLeft, out hit5, airDistance))
        {
            List<RaycastHit> raycastHitList = new List<RaycastHit> { hit, hit1, hit2, hit3, hit4, hit5};

            foreach (var item in raycastHitList)
            {
                if (item.collider != null)
                {
                    if (item.collider.CompareTag("Ground"))
                    {
                        playerMovement.AirGroundCollision(item.collider.gameObject);
                        break;
                    }
                    if (item.collider.CompareTag("Wall"))
                    {
                        playerMovement.AirWallCollision(item.collider.gameObject);
                        break;
                    }
                }
            }
        }
        else
        {
            playerMovement.AirWallUnCollision();
        }
    }
}