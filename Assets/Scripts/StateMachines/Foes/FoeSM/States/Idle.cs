using UnityEngine;

namespace StateMachines.FoeSM.States
{
    public class Idle<T> : BaseState where T : BaseFoeSm<T>
    {
        private readonly T sm;
        private float tmpTimer;

        public Idle(T stateMachine) : base($"FoeSM Idle ({typeof(T)})", stateMachine)
        {
            sm = stateMachine;
        }

        public override void Enter()
        {
            base.Enter();
            tmpTimer = sm.timer;
            sm.animator.SetFloat("hSpeed", 0);
            sm.animator.SetFloat("vSpeed", 0);
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
            
            if (sm.IsPlayerDetected)
            {
                sm.ChangeState(sm.GetCombatState());
                return;
            }

            if (sm.patrolPath.PathPoints is not null && sm.patrolPath.PathPoints.Length > 1)
            {
                sm.ChangeState(sm.foeStatesCont.GetState<Patrol<T>>());
                return;
            }
        }

        public override void UpdatePhysics()
        {
           
            base.UpdatePhysics();
            if (tmpTimer > 0)
            {
                tmpTimer -= Time.deltaTime;
            }
            else
            {
                tmpTimer = sm.timer;
                TurnAround();
            }
        }
        
        private void TurnAround()
        {
            sm.flip *= -1;
            var transform = sm.transform;
            var theScale = transform.localScale;
            theScale.z *= -1;
            transform.localScale = theScale;
        }
    }
}