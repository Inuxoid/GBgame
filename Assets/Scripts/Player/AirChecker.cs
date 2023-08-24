using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using StateMachines.PlayerSM;
using UnityEngine;
using UnityEngine.Events;

public class AirChecker : MonoBehaviour
{

    [Header("Components")]
    [SerializeField] PlayerSM sm;

    [Header("Settings")]
    [SerializeField] private float airDistance;
    [SerializeField] private Vector3 bigCast = new Vector3(0.3f, 1.83f, 0.7f);
    [SerializeField] private Vector3 smallCast = new Vector3(0.3f, 1.44f, 0.7f);
    [SerializeField] private Vector3 nullCast = Vector3.zero;
    [SerializeField] private Vector3 curCast;

    // private void OnDrawGizmosSelected()
    // {
    //     if (sm.CurrentState == sm.CrouchState)
    //         curCast = nullCast;
    //     else if (sm.isGrounded)
    //         curCast = bigCast;
    //     else
    //         curCast = smallCast;
    //     Gizmos.color = Color.blue;
    //     Gizmos.DrawWireCube(new Vector3(this.transform.position.x +
    //                         (float)sm.flip / 3,
    //                         this.transform.position.y - 0.18f,
    //                         this.transform.position.z),
    //                         curCast);
    // }

    private void Update()
    {
        // if (sm.CurrentState == sm.CrouchState)
        //     curCast = nullCast;
        // else if (sm.isGrounded)
        //     curCast = bigCast;
        // else
        //     curCast = smallCast;
        //
        //
        // List<Collider> colliders = Physics.OverlapBox(new Vector3(this.transform.position.x + 
        //                                                           (float)sm.flip / 3, 
        //                                                           this.transform.position.y - 0.18f, 
        //                                                           this.transform.position.z),
        //                                                           curCast / 2).Where(x => (x.GetComponent<ClimbData>() != null || x.CompareTag("Wall")) && 
        //                                                           Math.Abs(x.transform.position.x - transform.position.x) > 0.6f).ToList();

        /*//Debug.Log(colliders.Count);
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
        }*/
    }
}