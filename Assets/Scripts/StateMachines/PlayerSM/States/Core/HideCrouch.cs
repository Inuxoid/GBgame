using System;
using UnityEngine;

namespace StateMachines.PlayerSM.States
{
    public class HideCrouch : BaseState
    {
        private PlayerSM sm;
        private Color cachedColor;

        public HideCrouch(PlayerSM stateMachine) : base("HideCrouch", stateMachine)
        {
            sm = ((PlayerSM)stateMachine);
        }

        public override void Enter()
        {
            base.Enter();
            sm.animator.SetBool("isHardStopped", false); 
            sm.animator.SetBool("isHidden", true);
            // foreach (var foe in sm.foes)
            // {
            //     cachedColor = foe.vision.GetComponent<SpriteRenderer>().color;
            //     foe.vision.GetComponent<SpriteRenderer>().color = foe.visionColorInHide;
            // }
            sm.outline.OutlineColor = sm.outlineColor;
            sm.outline.OutlineMode = 0;
            sm.IsHidden = true;
            var transformPosition = sm.transform.position;
            transformPosition.z += 3.5f;
            sm.transform.position = transformPosition;
            //sm.outline.OutlineColor = Color.white;
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();

            if (sm.playerInput.actions["Move"].ReadValue<Vector2>().x == 0)
            {
                sm.ChangeState(sm.HideIdleState);
                return;
            }
        }
        
        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
            if (Math.Abs(sm.playerInput.actions["Move"].ReadValue<Vector2>().x) < 0.01f)
                sm.rb.velocity = Vector3.zero;
            
            sm.animator.SetFloat("hSpeed", Math.Abs(sm.playerInput.actions["Move"].ReadValue<Vector2>().x));
            sm.animator.SetFloat("vSpeed", Math.Abs(sm.rb.velocity.y));
            
            var direction = new Vector2(sm.playerInput.actions["Move"].ReadValue<Vector2>().x, sm.rb.velocity.y);
            sm.rb.velocity = direction * sm.hideSpeed;

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
            sm.animator.SetBool("isHidden", false);
            foreach (var foe in sm.foes)
            {
                foe.vision.GetComponent<SpriteRenderer>().color = cachedColor;
            }
            sm.outline.OutlineColor = Color.clear;
            sm.outline.OutlineMode += 1;
            sm.IsHidden = false;
            var transformPosition = sm.transform.position;
            transformPosition.z -= 3.5f;
            sm.transform.position = transformPosition;
            base.Exit();
        }
    }
}