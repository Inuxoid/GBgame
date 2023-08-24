using System;
using System.Linq;
using System.Threading.Tasks;
using StateMachines.FoeSM.States;
using UnityEngine;

namespace StateMachines.PlayerSM.States
{
    public class Idle : BaseState
    {
        private PlayerSM sm;
        private bool canRun = true;
        private bool holdJump;

        public Idle(PlayerSM stateMachine) : base("Idle", stateMachine)
        {
            sm = ((PlayerSM)stateMachine);
        }

        public override void Enter()
        {
            base.Enter();
            if (sm.willDead)
            {
                sm.liveCycle.GetDamage(100);
            }
            //sm.animator.SetBool("isHardStopped", false);
           // Debug.LogError("IDLE???");
            sm.isJumpButtonIsPressed = sm.playerInput.actions["Jump"].IsPressed();
            holdJump = sm.isJumpButtonIsPressed;
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
            canRun = true;
            if (raycast)
            {
                bool compareTag = hit.collider.CompareTag("Wall");
                bool foeSm = hit.collider.GetComponent<FoeSM.FoeSM>();
                bool back = sm.flip * sm.playerInput.actions["Move"].ReadValue<Vector2>().x < 0;
                canRun = !(compareTag || foeSm) || back;
            }

            if (Math.Abs(sm.playerInput.actions["Move"].ReadValue<Vector2>().x) > 0.2f && canRun && sm.canMoveAfterFalling)
            {
                sm.ChangeState(sm.RunState);
                return;
            }

            if ((sm.playerInput.actions["Jump"].IsPressed() || Input.GetKeyDown(KeyCode.W))
                && sm.canMoveAfterFalling
                && sm.isGrounded 
                && !sm.isJumpButtonWasPressed
                && !holdJump)
            {

                sm.ChangeState(sm.JumpState);
                return;
            }

            if (sm.playerInput.actions["Attack"].IsPressed())
            {
                //Debug.LogError("Attack");
                sm.ChangeState(sm.AttackState);
                return;
            }

            if (sm.playerInput.actions["Action"].IsPressed() && sm.CanHide) // TODO change to flag call from foe??
            {
                if (sm.detected)
                {
                    return;
                }
                sm.ChangeState(sm.HideIdleState);
                return;
            }
            else if (sm.playerInput.actions["Action"].IsPressed() && !sm.canAction)
            {
                sm.ChangeState(sm.CrouchState);
                return;
            }

            // if (sm.rb.velocity.y < -0.1f && !sm.isGrounded)
            // {
            //     sm.ChangeState(sm.FallingState);
            //     return;
            // }
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
            sm.rb.velocity = new Vector3(0, sm.rb.velocity.y, 0);
            sm.animator.SetFloat("hSpeed", 0);
            sm.animator.SetFloat("vSpeed", 0);
        }
    }
}