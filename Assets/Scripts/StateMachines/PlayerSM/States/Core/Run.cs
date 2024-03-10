using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using StateMachines.FoeSM.States;
using UnityEngine;

namespace StateMachines.PlayerSM.States
{
    public class Run : BaseState
    {
        private PlayerSM sm;
        private bool justEntered;
        private bool holdJump;
        private float dump;
 
        public Run(PlayerSM stateMachine) : base("Run", stateMachine)
        {
            sm = ((PlayerSM)stateMachine);
        }

        public override void Enter()
        {
            base.Enter();
            dump = 0.80f;
            sm.isJumpButtonIsPressed = sm.playerInput.actions["Jump"].IsPressed();
            holdJump = sm.isJumpButtonIsPressed;
            sm.cachedInput = sm.playerInput.actions["Move"].ReadValue<Vector2>().x;
            EnterTimer();
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();

            if (!sm.playerInput.actions["Jump"].IsPressed())
            {
                holdJump = false;
            }

            var rayForward = new Ray(sm.transform.position, new Vector3(sm.flip, 0));

            bool raycast = Physics.BoxCast(sm.transform.position, sm.halfExtents, 
                new Vector3(sm.flip, 0f, 0f), out var hit, Quaternion.identity, sm.maxDistanceToStop);
            if (raycast)
            {
                bool compareTag = hit.collider.CompareTag("Wall");
                bool foeSm = hit.collider.GetComponent<FoeSM.FoeSM>();
                bool back = sm.flip * sm.playerInput.actions["Move"].ReadValue<Vector2>().x < 0;
                bool canRun = !(compareTag || foeSm) || back;
                
                if (!canRun)
                {
                    sm.ChangeState(sm.IdleState);
                    return;
                }
            }
            
            if ((sm.playerInput.actions["Jump"].IsPressed() || Input.GetKeyDown(KeyCode.W)) 
                && sm.isGrounded 
                && !sm.isJumpButtonWasPressed
                && !holdJump)
            {

                sm.ChangeState(sm.JumpState);
                return;
            }

            if (sm.rb.velocity is { y: 0, x: 0 } 
                || (Math.Abs(sm.playerInput.actions["Move"].ReadValue<Vector2>().x) < 0.2f && sm.rb.velocity.y == 0))
            {
                sm.ChangeState(sm.IdleState);
                return;
            }
            
            
            if (sm.rb.velocity.y < -1f)
            {
                sm.currentFallingHeight = sm.transform.position.y;
                sm.ChangeState(sm.FallingState);
                return;
            }
            
            if (sm.playerInput.actions["Attack"].IsPressed())
            {
                sm.rb.velocity = Vector3.zero;
                sm.animator.SetFloat("hSpeed", 0);
                sm.ChangeState(sm.AttackState);
                return;
            }
            
            if (sm.playerInput.actions["Action"].IsPressed() && sm.CanHide)
            {
                if (sm.detected)
                {
                    return;
                }
                sm.rb.velocity = Vector3.zero;
                sm.animator.SetFloat("hSpeed", 0);
                sm.ChangeState(sm.HideIdleState);
                return;
            } 
            else if (sm.playerInput.actions["Action"].IsPressed() && !sm.canAction)
            {
                sm.ChangeState(sm.CrouchState);
                return;
            }
            
            if (sm.playerInput.actions["Roll"].IsPressed())
            {
                sm.ChangeState(sm.SomersaultState);
            }
            
            CheckClimb();
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();

            // sm.animator.SetFloat("hSpeed", Math.Abs(sm.playerInput.actions["Move"].ReadValue<Vector2>().x) * 100 * dump);
            sm.animator.SetFloat("hSpeed", Math.Abs(sm.playerInput.actions["Move"].ReadValue<Vector2>().x) * dump);
            sm.animator.SetFloat("vSpeed", Math.Abs(sm.rb.velocity.y));
            
            var direction = sm.playerInput.actions["Move"].ReadValue<Vector2>().x;
            // sm.rb.velocity = new Vector3(direction * sm.runSpeed * dump, sm.rb.velocity.y);
            sm.rb.velocity = new Vector3(direction * sm.runSpeed * dump, sm.rb.velocity.y);

            if (sm.playerInput.actions["Move"].ReadValue<Vector2>().x > 0 && !sm.facingRight ||
                sm.playerInput.actions["Move"].ReadValue<Vector2>().x < 0 && sm.facingRight)
            {
                sm.facingRight = !sm.facingRight;
                sm.transform.Rotate(0, 180, 0);
                sm.flip *= -1;
            }



            if (Math.Abs(sm.playerInput.actions["Move"].ReadValue<Vector2>().x) < 0.01f)
            {
                sm.rb.velocity = Vector3.zero;
            }

            sm.cachedInput = sm.playerInput.actions["Move"].ReadValue<Vector2>().x;
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

        public void EnterTimer()
        {
            Thread thread = new Thread(() =>
            {
                while (dump < 1f)
                {
                    dump += 0.05f;
                    Thread.Sleep(150);
                }
            });
            thread.Start();
        }

        public override void Exit()
        {
            //Debug.Log(Math.Abs(sm.cachedInput) - sm.hardStopNeededSpeed);
            if (Math.Abs(sm.cachedInput) >= sm.hardStopNeededSpeed)
            {
                sm.animator.SetBool("isHardStopped", true); 
            }
            else
            {
                sm.cachedInput = 0f;
            }
            sm.cachedInput = 0;
            base.Exit();
        }
    }
}