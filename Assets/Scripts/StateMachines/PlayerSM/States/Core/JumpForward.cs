using System;
using System.Linq;
using UnityEngine;

namespace StateMachines.PlayerSM.States
{
    public class JumpForward : BaseState
    {
        private PlayerSM sm;
        
        public JumpForward(PlayerSM stateMachine) : base("JumpForward", stateMachine)
        {
            sm = ((PlayerSM)stateMachine);
        }
        
        public override void Enter()
        {
            base.Enter();
            sm.isGrounded = false;
            sm.animator.SetBool("isJumping", true);
            float jumpForce = Mathf.Sqrt(sm.jumpForwardHeight * -2 * (Physics2D.gravity.y) * 4);
            sm.rb.AddForce(new Vector2(sm.jumpForwardForceForwardMult,jumpForce), ForceMode.Impulse);
        }
        
        public override void UpdateLogic()
        {
            base.UpdateLogic();

            if (
                //currentJumpTimer >= sm.maxJumpTimer 
                //|| 
                (sm.rb.velocity.y < -0.1f && !sm.isGrounded)
            )
            {
                sm.ChangeState(sm.FallingState);
            }
            CheckClimb();
        }
        
        private void CheckClimb()
        {
            var centerVector = new Vector3(
                sm.transform.position.x + (float)sm.flip / 3,
                sm.transform.position.y - 0.18f,
                sm.transform.position.z);

            var colliders = Physics.OverlapBox(centerVector, sm.curCast / 2)
                .Where(x => (x.GetComponent<ClimbData>() != null || x.CompareTag("Wall"))
                            && Math.Abs(x.transform.position.x - sm.transform.position.x) > 0.6f).ToList();

            if (colliders.Count == 0) return;

            foreach (var item in colliders)
            {
                if (!item.CompareTag("Ground")) continue;
                
                if (!(sm.playerInput.actions["Move"].ReadValue<Vector2>().x *
                        (item.transform.position.x - sm.transform.position.x) > 0)) continue;
                
                var left = !((item.transform.position.x - sm.transform.position.x) < 0);
                
                if (!item.TryGetComponent(out ClimbData climbData) ||
                    !sm.climb.IsCanClimb(climbData.FirstPoint(left))) continue;
                
                sm.ClimbState.points = climbData.Points(left);
                
                sm.ChangeState(sm.ClimbState);
                return;
            }

            if (colliders.Any(item => item.CompareTag("Wall")))
            {
                sm.ChangeState(sm.FallingState);
            }
        }
        
        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
            
            sm.animator.SetFloat("hSpeed", Math.Abs(sm.rb.velocity.x) * 50);
            sm.animator.SetFloat("vSpeed", Math.Abs(sm.rb.velocity.y));
            
            var direction = sm.playerInput.actions["Move"].ReadValue<Vector2>().x;
            //sm.rb.velocity = new Vector3(direction * sm.runSpeed * 0.8f, sm.rb.velocity.y);

            if (sm.playerInput.actions["Move"].ReadValue<Vector2>().x > 0 && !sm.facingRight ||
                sm.playerInput.actions["Move"].ReadValue<Vector2>().x < 0 && sm.facingRight)
            {
                sm.facingRight = !sm.facingRight;
                sm.transform.Rotate(0, 180, 0);
                sm.flip *= -1;
            }
        }
        
        public override void Exit()
        {
            sm.animator.SetBool("isJumping", false);
            base.Exit();
        }        
    }
}