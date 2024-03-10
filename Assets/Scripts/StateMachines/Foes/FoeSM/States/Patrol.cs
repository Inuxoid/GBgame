using System;
using UnityEngine;

namespace StateMachines.FoeSM.States
{
    public class Patrol<T> : BaseState where T : BaseFoeSm<T>
    {
        private readonly T sm;

        public Patrol(T stateMachine)  : base($"FoeSM Patrol ({typeof(T)})", stateMachine)
        {
            sm = stateMachine; 
        }

        public Transform target;
        private int nextTargetInd;
        private const float TargetDistance = 1f;

        public override void Enter()
        {
            base.Enter();
            target = sm.patrolPath.PathPoints[0];
            nextTargetInd = 1;
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
            if (sm.IsPlayerDetected)                    
            {                                                              
                sm.ChangeState(sm.GetCombatState());
                return;                                                    
            }                                                              
        }

        public override void UpdatePhysics()
        {
            //Debug.LogError("Patrolling");
            MoveToPatrolTarget();
        }

        private void MoveToPatrolTarget()
        {
            if (sm.animator.GetBool("isTurning"))
            {
                sm.animator.SetFloat("hSpeed", 0f);
                sm.rigidbody.velocity = Vector2.zero;
                return;
            }
            
            var normalizedX = new Vector2(target.transform.position.x - sm.transform.position.x, 0).normalized.x;
            sm.rigidbody.velocity = new Vector2(sm.enemySpeed * normalizedX, 0);
            //Debug.LogError($"Speed in MTPT: {sm.rigidbody.velocity}");
            //Debug.Log($"{Math.Abs(sm.transform.position.x - target.position.x)}");
            sm.animator.SetFloat("hSpeed", 1f);
            
            if (Math.Abs(sm.transform.position.x - target.position.x) < TargetDistance
                || (target.position.x - sm.transform.position.x) * sm.flip < 0)
            {
                TurnAround();
            }
        }
        
        private void TurnAround()
        {
            if (sm.flip * (target.position.x - sm.transform.position.x) < 0 && sm.animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Sword And Shield 180 Turn")
            {
                sm.animator.SetBool("isTurning", true);
                sm.animator.Play("Sword And Shield 180 Turn");
            }
            
            //Debug.LogError($"Turn around");
            if (sm.patrolPath.PathPoints.Length == 1 && Math.Abs(sm.transform.position.x - target.position.x) < TargetDistance)
            {
                sm.ChangeState(sm.foeStatesCont.GetState<Idle<T>>());
                return;
            }

            if (sm.patrolPath.PathPoints.Length == 2)
            {
                target = sm.patrolPath.PathPoints[nextTargetInd];
                nextTargetInd = nextTargetInd == 1 ? 0 : 1;
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}   