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
            
            if (sm.patrolPath.PathPoints.Length == 2) // Check if foe has route or just staying
            { 
                var position = sm.transform.position;
                var distanceToPoint1 = Vector2.Distance(sm.player.transform.position, sm.leftBorder.transform.position);
                var distanceToPoint2 = Vector2.Distance(sm.player.transform.position, sm.rightBorder.transform.position);


                // Set first point to move for patrol - the most close border to player
                seekTarget = distanceToPoint1 < distanceToPoint2 
                        ? sm.leftBorder.transform 
                        : sm.rightBorder.transform;
            }
            else
            {
                sm.foeStatesCont.GetState<Patrol<T>>().target = sm.patrolPath.PathPoints[0];
            }
            
            // if (sm.flip * (sm.foeStatesCont.GetState<Patrol<T>>().target.position.x - sm.transform.position.x) < 0)
            // {
            //     sm.flip *= -1;
            //     var theScale = sm.transform.localScale;
            //     theScale.z *= -1;
            //     sm.transform.localScale = theScale;
            // }
            
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