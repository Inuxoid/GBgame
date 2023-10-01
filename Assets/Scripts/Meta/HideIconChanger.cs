using StateMachines.PlayerSM;
using UI;
using UnityEngine;

namespace Meta
{
    public class HideIconChanger : MonoBehaviour
    {
        [SerializeField] private ActionButton buttonIconUpdater;

        private void Awake()
        {
            buttonIconUpdater = FindObjectOfType<ActionButton>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponentInParent<PlayerSM>())
            {
                buttonIconUpdater.OnHideNear(true);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponentInParent<PlayerSM>())
            {
                buttonIconUpdater.OnHideNear(false);
            }
        }
    }
}