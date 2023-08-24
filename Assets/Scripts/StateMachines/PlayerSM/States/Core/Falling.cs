using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StateMachines.PlayerSM.States
{
    public class Falling : BaseState
    {
        private PlayerSM sm;
        private bool canRun = true;
        
        public Falling(PlayerSM stateMachine) : base("Falling", stateMachine)
        {
            sm = stateMachine;
        }

        public override void Enter()
        {
            base.Enter();
            //Debug.Log(sm.transform.position.y);
            sm.animator.SetBool("isFalling", true);
            sm.animator.SetBool("isHardFalling", false);
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();

            if (sm.isGrounded && Math.Abs(sm.rb.velocity.y) <= 0.01f && !sm.liveCycle.isDead)
            {
                sm.onLanding.Invoke();
                FMODUnity.RuntimeManager.PlayOneShot("event:/jump");
                sm.ChangeState(sm.IdleState);
                return;
            }

            CheckClimb();
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
            sm.animator.SetFloat("hSpeed", Math.Abs(sm.rb.velocity.x) * 50);
            sm.animator.SetFloat("vSpeed", Math.Abs(sm.rb.velocity.y));
            
            var rayForward = new Ray(sm.transform.position, new Vector3(sm.flip, 0));
            bool raycast = Physics.Raycast(rayForward, out var hit, 1f);
            canRun = true;
            if (raycast)
            {
                bool compareTag = hit.collider.CompareTag("Wall");
                bool foeSm = hit.collider.GetComponent<FoeSM.FoeSM>();
                bool back = sm.flip * sm.playerInput.actions["Move"].ReadValue<Vector2>().x < 0;
                canRun = !(compareTag || foeSm) || back;
                //Debug.Log( $"({compareTag} || {foeSm}) || {back}");
            }
            
            if (canRun)
            {
                var direction = sm.playerInput.actions["Move"].ReadValue<Vector2>().x;
                sm.rb.velocity = new Vector3(direction * sm.runSpeed * sm.fallingXSpeed, sm.rb.velocity.y);
            }


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
            sm.currentFallingHeight -= sm.transform.position.y;
            if (sm.currentFallingHeight > sm.deathFallingHeight)
            {
                sm.willDead = true;
            }

            if (sm.currentFallingHeight > sm.maxFallingHeight)
            {
                sm.animator.SetBool("isHardFalling", true);
                sm.canMoveAfterFalling = false;
            }            
            sm.animator.SetBool("isFalling", false);
            //sm.isGrounded = true;
            sm.animator.SetBool("isHardStopped", false);
            
            base.Exit();
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
        }
    }
}