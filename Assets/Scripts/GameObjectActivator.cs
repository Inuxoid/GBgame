using StateMachines.PlayerSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectActivator : MonoBehaviour
{
    [SerializeField] private GameObject Object;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Object.SetActive(true);
        }
    }
}
