using UnityEngine;

namespace StateMachines.Foes.FoeRangeSM
{
    public class HitTriggerRangeFoe : MonoBehaviour
    {
        [SerializeField] private StateMachines.FoeRangeSM.FoeRangeSM foeRangeSmSm;
        
        public void Hit()
        {
            foeRangeSmSm.GetCombatState().Hit();
        }
        
        public void Shoot()
        {
            foeRangeSmSm.GetCombatState().RangeHit();
        }
    }
}