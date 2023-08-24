using System;
using UnityEngine;

namespace StateMachines.PlayerSM.States
{
    public class Crouch : BaseState
    {
        private PlayerSM sm;

        [SerializeField] private readonly Vector3 smallColl = new Vector3(0.6f, 0.6f, 0.6f);
        [SerializeField] private readonly Vector3 normColl = new Vector3(1f, 1f, 1f);


        public Crouch(PlayerSM stateMachine) : base("Crouch", stateMachine)
        {
            sm = ((PlayerSM)stateMachine);
        }

        public override void Enter()
        {
            base.Enter();
            sm.animator.SetBool("isHardStopped", false); 
            sm.animator.SetBool("isCrouch", true);
            sm.bodyCollider.transform.localScale = smallColl;
            sm.visionCollider.transform.localScale = smallColl;
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();

            if (!sm.playerInput.actions["Action"].IsPressed() && sm.canStandUp)
            {
                sm.ChangeState(sm.IdleState);
                return;
            }
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
            sm.animator.SetFloat("hSpeed", Math.Abs(sm.playerInput.actions["Move"].ReadValue<Vector2>().x));
            sm.animator.SetFloat("vSpeed", Math.Abs(sm.rb.velocity.y));
            
            var direction = sm.playerInput.actions["Move"].ReadValue<Vector2>().x;
            sm.rb.velocity = new Vector3(direction * sm.crouchSpeed, sm.rb.velocity.y);

            if (sm.playerInput.actions["Move"].ReadValue<Vector2>().x > 0 && !sm.facingRight ||
                sm.playerInput.actions["Move"].ReadValue<Vector2>().x < 0 && sm.facingRight)
            {
                sm.facingRight = !sm.facingRight;
                sm.transform.Rotate(0, 180, 0);
                sm.flip *= -1;
            }
            
            var rayUp = new Ray(sm.transform.position, Vector3.up);
            bool raycast = Physics.Raycast(rayUp, out var hit, 0.5f);
            if (raycast)
            {
                sm.canStandUp = !hit.collider.CompareTag("Ceil");
            }
            else
            {
                sm.canStandUp = true;
            }
        }

        public override void Exit()
        {
            sm.animator.SetBool("isCrouch", false);
            sm.bodyCollider.transform.localScale = normColl;
            sm.visionCollider.transform.localScale = normColl;
            base.Exit();
        }
    }
}