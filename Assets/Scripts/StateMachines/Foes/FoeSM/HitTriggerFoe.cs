
using UnityEngine;

namespace StateMachines.Foes.FoeSM
{
    public class HitTriggerFoe : MonoBehaviour
    {
        [SerializeField] private StateMachines.FoeSM.FoeSM foeSm;

        private void Awake()
        {
            GetComponent<StateMachines.FoeSM.FoeSM>();
        }

        public void Hit()
        {
            foeSm.GetCombatState().Hit();
        }
    }
}