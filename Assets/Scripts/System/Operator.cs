using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Operator : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float X;
    [SerializeField] private float Y;
    [SerializeField] private float Z;
    [SerializeField] private GameObject FollowPiont;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FollowPiont.transform.position = new Vector3(FollowPiont.transform.position.x + X, FollowPiont.transform.position.y + Y, FollowPiont.transform.position.z + Z);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FollowPiont.transform.position = new Vector3(FollowPiont.transform.position.x - X, FollowPiont.transform.position.y - Y, FollowPiont.transform.position.z - Z);
        }
    }
}
