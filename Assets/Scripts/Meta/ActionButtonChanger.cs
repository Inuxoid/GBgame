using StateMachines.PlayerSM;
using UI;
using UnityEngine;

namespace Meta
{
    public class ActionButtonChanger : MonoBehaviour
    {
        [SerializeField] private ActionButton buttonIconUpdater;

        private void Awake()
        {
            buttonIconUpdater = FindObjectOfType<ActionButton>();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.GetComponentInParent<PlayerSM>())
            {
                buttonIconUpdater.OnActionNear(true);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponentInParent<PlayerSM>())
            {
                buttonIconUpdater.OnActionNear(false);
            }
        }
    }
}