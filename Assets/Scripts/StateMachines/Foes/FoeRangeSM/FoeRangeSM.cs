using StateMachines.FoeSM.States;
using StateMachines.PlayerSM.States;
using UnityEngine;

namespace StateMachines.FoeRangeSM
{
    public class FoeRangeSM : BaseFoeSm<FoeRangeSM>
    {
        protected override BaseState GetInitialState()
        {
            return foeStatesCont.GetState<Idle<FoeRangeSM>>();
        }

        public override CombatState<FoeRangeSM> GetCombatState()
        {
            return foeStatesCont.GetState<RangeFight<FoeRangeSM>>();
        }
    }
}