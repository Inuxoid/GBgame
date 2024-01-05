using System;
using Let.Foes;
using StateMachines.FoeSM.States;
using UnityEngine;
using UnityEngine.Events;
using Dto;
using StateMachines.PlayerSM.States;
using UnityEngine.Serialization;

namespace StateMachines.FoeSM
{
    public class FoeSM : BaseFoeSm<FoeSM>
    {
        protected override BaseState GetInitialState()
        {
            return foeStatesCont.GetState<Idle<FoeSM>>();
        }

        public override CombatState<FoeSM> GetCombatState()
        {
            return foeStatesCont.GetState<Fight<FoeSM>>();
        }
    }
}