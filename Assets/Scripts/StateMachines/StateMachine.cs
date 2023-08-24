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
        }

        protected virtual BaseState GetInitialState()
        {
            return null;
        }
    }
}