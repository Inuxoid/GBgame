using System;
using System.Collections.Generic;
using StateMachines.Foes.FoeShieldSM.States;
using StateMachines.FoeSM.States;
using UnityEngine;

namespace StateMachines
{
    public class FoeStatesCont<T> where T : BaseFoeSm<T>
    {
        private T sm;
        private readonly Dictionary<Type, BaseState> states;
        
        public FoeStatesCont(T baseFoeSm)
        {
            sm = baseFoeSm;
            states = new Dictionary<Type, BaseState>(20);
            CreateIdleState();
            CreateFightState();
            CreateRangeFightState();
            CreateShieldFightState();
            CreateDeathState();
            CreatePatrolState();
            CreateSeekState();
        }
        
        public TState GetState<TState>() where TState : BaseState
        {
            var requestedType = typeof(TState);

            if (states.ContainsKey(requestedType))
            {
                return (TState)states[requestedType];
            }
            Debug.Log($"State of type {requestedType} not found in states dictionary.");
            throw new ArgumentNullException();
        }
        
        private void CreateIdleState()
        {
            Idle<T> state = new Idle<T>(sm);
            SaveState(state);
        }
        
        private void CreateFightState()
        {
            Fight<T> state = new Fight<T>(sm);
            SaveState(state);
        }        
        
        private void CreateRangeFightState()
        {
            RangeFight<T> state = new RangeFight<T>(sm);
            SaveState(state);
        }
        
        private void CreateShieldFightState()
        {
            ShieldFight<T> state = new ShieldFight<T>(sm);
            SaveState(state);
        }

        private void CreateDeathState()
        {
            Death<T> state = new Death<T>(sm);
            SaveState(state);
        }
        
        private void CreatePatrolState()
        {
            Patrol<T> state = new Patrol<T>(sm);
            SaveState(state);
        }

        private void CreateSeekState()
        {
            Seek<T> state = new Seek<T>(sm);
            SaveState(state);
        }
        
        private void SaveState(BaseState state) => 
            states.Add(state.GetType(), state);
    }
}