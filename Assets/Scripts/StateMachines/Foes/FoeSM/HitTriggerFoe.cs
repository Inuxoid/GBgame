
using UnityEngine;

namespace StateMachines.Foes.FoeSM
{
    public class HitTriggerFoe : MonoBehaviour
    {
        [SerializeField] private StateMachines.FoeSM.FoeSM foeSm;
        
        public void Hit()
        {
            foeSm.GetCombatState().Hit();
        }
    }
}