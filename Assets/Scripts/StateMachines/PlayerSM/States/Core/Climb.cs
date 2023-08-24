using UnityEngine;

namespace StateMachines.PlayerSM.States
{
    public class Climb : BaseState
    {
        private PlayerSM sm;
        public Vector3[] points;

        
        public Climb(PlayerSM stateMachine) : base("Climb", stateMachine)
        {
            sm = ((PlayerSM)stateMachine);
        }

        public override void Enter()
        {
            base.Enter();
            sm.customGravity.gravityScale = 0f;
            sm.cachedInput = 0f;
            sm.animator.SetBool("isJumping", false);
            //sm.rb.useGravity = false;
            sm.climb.StartClimb(points);
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
            if (!sm.climb.StartedClimbing)
            {
                sm.ChangeState(sm.RunState);
            }
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
           //sm.rb.velocity = Vector3.zero;
        }
        
        public override void Exit()
        {
            sm.cachedInput = 0f;
            sm.animator.SetBool("isHardFalling", false);
            sm.animator.SetBool("isHardStopped", false);
            sm.canMoveAfterFalling = true;
            sm.customGravity.gravityScale = 4f;
            base.Exit();
            //sm.animator.SetBool("isClimbing", false);
            //sm.rb.useGravity = true;
        }
    }
}