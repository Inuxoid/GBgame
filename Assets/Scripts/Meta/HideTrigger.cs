using System;
using StateMachines.PlayerSM;
using UnityEngine;

namespace Meta
{
    public class HideTrigger : MonoBehaviour
    {
        [SerializeField] private PlayerSM sm;
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponentInParent<PlayerSM>())
                sm.canHide = true;
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponentInParent<PlayerSM>())
                sm.canHide = false;
        }
    }
}