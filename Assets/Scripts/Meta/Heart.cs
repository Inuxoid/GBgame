using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Heart : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int hpRes;

    private void OnTriggerEnter(Collider other)
    {
        LiveCycle liveCycle = other.GetComponentInParent<LiveCycle>();
        if (other.CompareTag("Player"))
        {
            other.GetComponentInParent<LiveCycle>().GetHeart(hpRes);
            Destroy(gameObject);
        }
    }
}
