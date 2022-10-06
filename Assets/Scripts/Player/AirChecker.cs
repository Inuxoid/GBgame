using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Progress;

public class AirChecker : MonoBehaviour
{

    [Header("Components")]
    [SerializeField] PlayerMovement playerMovement;

    [Header("Settings")]
    [SerializeField] private float airDistance;
    [SerializeField] private Vector3 bigCast = new Vector3(0.3f, 1.83f, 0.7f);
    [SerializeField] private Vector3 smallCast = new Vector3(0.3f, 1.44f, 0.7f);
    [SerializeField] private Vector3 nullCast = Vector3.zero;
    [SerializeField] private Vector3 curCast;

    private void OnDrawGizmosSelected()
    {
        if (playerMovement.Crouch)
            curCast = nullCast;
        else if (playerMovement.IsGrounded)
            curCast = bigCast;
        else
            curCast = smallCast;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(this.transform.position.x +
                            (float)playerMovement.IntFlip / 3,
                            this.transform.position.y - 0.18f,
                            this.transform.position.z),
                            curCast);
    }

    private void Update()
    {
        if (playerMovement.Crouch)  
            curCast = nullCast;
        else if (playerMovement.IsGrounded)
            curCast = bigCast;
        else 
            curCast = smallCast;


        List<Collider> colliders = Physics.OverlapBox(new Vector3(this.transform.position.x + 
                                                                  (float)playerMovement.IntFlip / 3, 
                                                                  this.transform.position.y - 0.18f, 
                                                                  this.transform.position.z),
                                                                  curCast / 2).Where(x => (x.GetComponent<ClimbData>() != null || x.CompareTag("Wall")) && 
                                                                  Math.Abs(x.transform.position.x - transform.position.x) > 0.6f).ToList();


        //RaycastHit hit = new(); 
        //RaycastHit hit1 = new();
        //RaycastHit hit2 = new();
        //RaycastHit hit3 = new();
        //RaycastHit hit4 = new();
        //RaycastHit hit5 = new();


        //Ray airRayTopRight = new Ray(new Vector3(transform.position.x, transform.position.y + 0.7f), Vector3.right);
        //Ray airRayBotRight = new Ray(new Vector3(transform.position.x, transform.position.y - 0.5f), Vector3.right);
        //Ray airRayRight = new Ray(transform.position, Vector3.right);

        //Ray airRayTopLeft = new Ray(new Vector3(transform.position.x, transform.position.y + 0.7f), Vector3.left);
        //Ray airRayBotLeft = new Ray(new Vector3(transform.position.x, transform.position.y - 0.5f), Vector3.left);
        //Ray airRayLeft = new Ray(transform.position, Vector3.left);

        Debug.Log(colliders.Count);
        if (colliders.Count != 0)
        {
            bool _isFounded = false;
            foreach (var item in colliders)
            {
                if (item.CompareTag("Ground"))
                {
                    playerMovement.AirGroundCollision(item.gameObject);
                    _isFounded = true;
                    break;
                }
            }

            if (!_isFounded)
            {
                foreach (var item in colliders)
                {
                    if (item.CompareTag("Wall"))
                    {
                        playerMovement.AirWallCollision(item.gameObject);
                        break;
                    }
                }
            }
        }
        else
        {
            playerMovement.AirWallUnCollision();
        }


        //if (Physics.Raycast(airRayRight, out hit, airDistance) ||
        //    Physics.Raycast(airRayLeft, out hit1, airDistance) ||
        //    Physics.Raycast(airRayTopRight, out hit2, airDistance) ||
        //    Physics.Raycast(airRayBotRight, out hit3, airDistance) ||
        //    Physics.Raycast(airRayTopLeft, out hit4, airDistance) ||
        //    Physics.Raycast(airRayBotLeft, out hit5, airDistance))
        //{
        //    List<RaycastHit> raycastHitList = new List<RaycastHit> { hit, hit1, hit2, hit3, hit4, hit5};

        //    foreach (var item in raycastHitList)
        //    {
        //        if (item.collider != null)
        //        {
        //            if (item.collider.CompareTag("Ground"))
        //            {
        //                playerMovement.AirGroundCollision(item.collider.gameObject);
        //                break;
        //            }
        //            if (item.collider.CompareTag("Wall"))
        //            {
        //                playerMovement.AirWallCollision(item.collider.gameObject);
        //                break;
        //            }
        //        }
        //    }
        //}
        //else
        //{
        //    playerMovement.AirWallUnCollision();
        //}
    }
}