using System.Collections.Generic;
using StateMachines.PlayerSM;
using TMPro;
using UnityEngine;

namespace StateMachines
{
    public class StateMachine : MonoBehaviour
    {
        public BaseState CurrentState { get; private set; }

        protected void Start()
        {
            CurrentState = GetInitialState();
            CurrentState?.Enter();
        }

        protected void Update()
        {
            CurrentState?.UpdateLogic();
        }        
        
        protected void LateUpdate()  
        {
            CurrentState?.UpdatePhysics();
        }

        public void ChangeState(BaseState newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
            NotifyStateObservers(newState);
        }

        protected virtual BaseState GetInitialState()
        {
            return null;
        }
        
        private List<IPlayerStateObserver> stateObservers = new List<IPlayerStateObserver>();
        
        public void AddStateObserver(IPlayerStateObserver observer)
        {
            if (!stateObservers.Contains(observer))
            {
                stateObservers.Add(observer);
            }
        }

        public void RemoveStateObserver(IPlayerStateObserver observer)
        {
            stateObservers.Remove(observer);
        }
        
        private void NotifyStateObservers(BaseState newState)
        {
            foreach (var observer in stateObservers)
            {
                observer.OnPlayerStateChanged(newState);
            }
        }
    }
}