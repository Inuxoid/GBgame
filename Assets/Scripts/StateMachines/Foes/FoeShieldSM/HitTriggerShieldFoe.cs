using UnityEngine;
using UnityEngine.Serialization;

namespace StateMachines.Foes.FoeShieldSM
{
    public class HitTriggerShieldFoe : MonoBehaviour
    {
        [SerializeField] private FoeShieldSM shieldFoeSm;
        
        public void Hit()
        {
            shieldFoeSm.GetCombatState().Hit();
        }
    }
}