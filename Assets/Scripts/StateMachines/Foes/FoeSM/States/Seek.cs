using System;
using System.Linq;
using UnityEngine;

namespace StateMachines.FoeSM.States
{
    public class Seek<T> : BaseState where T : BaseFoeSm<T>
    {
        private readonly T sm;
        private float cachedSpeed;
        private Transform seekTarget;

        public Seek(T stateMachine) : base($"FoeSM Seek ({typeof(T)})", stateMachine)
        {
            sm = stateMachine; 
        }

        public override void Enter()
        {
            base.Enter();
            cachedSpeed = sm.enemySpeed;
            sm.enemySpeed *= 2f;
            Debug.Log("SEEK");
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
            if (sm.IsPlayerDetected)                    
            {
                Debug.Log("COMBAT");
                sm.ChangeState(sm.GetCombatState());
                return;                                                    
            }
            
            var position = sm.transform.position;
            var distanceToPoint1 = Vector2.Distance(sm.player.transform.position, sm.leftBorder.transform.position);
            var distanceToPoint2 = Vector2.Distance(sm.player.transform.position, sm.rightBorder.transform.position);


            // Set first point to move for patrol - the most close border to player
            seekTarget = distanceToPoint1 < distanceToPoint2 
                ? sm.leftBorder.transform 
                : sm.rightBorder.transform;
            
            const float closeDistance = 0.3f; 
            if (
                Math.Abs(sm.transform.position.x - sm.leftBorder.transform.position.x) < closeDistance ||
                Math.Abs(sm.transform.position.x - sm.rightBorder.transform.position.x) < closeDistance
                )
            {
                sm.ChangeState(sm.foeStatesCont.GetState<Patrol<T>>());
            }
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
            MoveToSeekTarget();
        }
        
        private void MoveToSeekTarget()
        {
            if (seekTarget == null)
            {
                return;
            }
            var normalizedX = new Vector2(seekTarget.transform.position.x - sm.transform.position.x, 0).normalized.x;
            sm.rigidbody.velocity = new Vector2(sm.enemySpeed * normalizedX, 0);
            sm.animator.SetFloat("hSpeed", 1f);
        }

        public override void Exit()
        {
            Debug.Log("EXIT");
            sm.enemySpeed = cachedSpeed;
            base.Exit();
        }
    }
}