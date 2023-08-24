namespace StateMachines.PlayerSM.States
{
    /// <summary>
    /// До востребования
    /// </summary>
    public class Impact : BaseState
    {
        private PlayerSM sm;

        public Impact(PlayerSM stateMachine) : base("Impact", stateMachine)
        {
            sm = ((PlayerSM)stateMachine);
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
        }
    }
}