using StateMachines.Foes.FoeShieldSM.States;
using StateMachines.FoeSM.States;
using UnityEngine;

namespace StateMachines.Foes.FoeShieldSM
{
    public class FoeShieldSM : BaseFoeSm<FoeShieldSM>
    {
        protected override BaseState GetInitialState()
        {
            return foeStatesCont.GetState<Idle<FoeShieldSM>>();
        }

        public override CombatState<FoeShieldSM> GetCombatState()
        {
            return foeStatesCont.GetState<ShieldFight<FoeShieldSM>>();
        }
    }
}