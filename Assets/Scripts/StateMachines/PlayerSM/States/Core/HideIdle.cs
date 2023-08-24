using UnityEngine;

namespace StateMachines.PlayerSM.States
{
    public class HideIdle : BaseState
    {
        private PlayerSM sm;
        private Color cachedColor;

        public HideIdle(PlayerSM stateMachine) : base("HideIdle", stateMachine)
        {
            sm = (PlayerSM)base.stateMachine;
        }

        public override void Enter()
        {
            base.Enter();
            sm.animator.SetBool("isHidden", true);
            foreach (var foe in sm.foes)
            {
                cachedColor = foe.vision.GetComponent<SpriteRenderer>().color;
                foe.vision.GetComponent<SpriteRenderer>().color = foe.visionColorInHide;
            }
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
            
            if (sm.playerInput.actions["Move"].ReadValue<Vector2>().x != 0)
            {
                sm.ChangeState(sm.HideCrouchState);
                return;
            }
            
            if (sm.playerInput.actions["Action"].IsPressed() && sm.CanUnHide)
            {
                sm.ChangeState(sm.IdleState);
                return;
            }
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
            sm.animator.SetFloat("hSpeed", 0);
            sm.animator.SetFloat("vSpeed", 0);
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