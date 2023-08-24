using StateMachines.PlayerSM;
using UnityEngine;

namespace Meta
{
    public class UnHideTrigger : MonoBehaviour
    { 
        [SerializeField] private PlayerSM sm;
        private void OnTriggerStay(Collider other)
        {
            sm.canUnHide = true;
        }
        
        private void OnTriggerExit(Collider other)
        {
            sm.canUnHide = false;
        }
    }
}