namespace StateMachines.FoeSM.States
{
    public class Listen<T> : BaseState where T : BaseFoeSm<T>
    {
        private readonly T sm;
        
        public Listen(T stateMachine) : base($"FoeSM Listen ({typeof(T)})", stateMachine)
        {
            sm = stateMachine;
        }

        public override void Enter()
        {
            base.Enter();
            sm.animator.SetBool("Listen", true);
            //TODO добавить вопросительный знак
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
            if (sm.IsPlayerDetected)
            {
                sm.ChangeState(sm.foeStatesCont.GetState<Fight<T>>());
            }
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
        }

        public override void Exit()
        {
            sm.animator.SetBool("Listen", false);
            base.Exit();
        }
    }
}