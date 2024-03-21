using FMOD;
using Debug = UnityEngine.Debug;

namespace StateMachines
{
    public class BaseState
    {
        public string name;
        protected StateMachine stateMachine;
        

        public BaseState(string name, StateMachine stateMachine)
        {
            this.name = name;
            this.stateMachine = stateMachine;
        }


        public virtual void Enter()
        {
            Debug.Log($"{name} enter");
        }
        public virtual void UpdateLogic(){}
        public virtual void UpdatePhysics(){}

        public virtual void Exit()
        {
            Debug.Log($"{name} exit");
        }
    }
}