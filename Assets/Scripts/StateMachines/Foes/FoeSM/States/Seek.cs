using System;
using System.Linq;
using UnityEngine;

namespace StateMachines.FoeSM.States
{
    public class Seek<T> : BaseState where T : BaseFoeSm<T>
    {
        private readonly T sm;

        public Seek(T stateMachine) : base($"FoeSM Seek ({typeof(T)})", stateMachine)
        {
            sm = stateMachine; 
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
            if (sm.IsPlayerDetected)                    
            {                                                              
                sm.ChangeState(sm.GetCombatState());
                return;                                                    
            }
            

            // TODO изменить на бег до крайней точки
            if (sm.patrolPath.PathPoints.Length == 2)
            { 
                var position = sm.transform.position;
                var distanceToPoint1 = Vector2.Distance(position, sm.patrolPath.PathPoints[0].position);
                var distanceToPoint2 = Vector2.Distance(position, sm.patrolPath.PathPoints[1].position);


                sm.foeStatesCont.GetState<Patrol<T>>().target = 
                    distanceToPoint1 < distanceToPoint2 
                        ? sm.patrolPath.PathPoints[0] 
                        : sm.patrolPath.PathPoints[1];
            }
            else
            {
                sm.foeStatesCont.GetState<Patrol<T>>().target = sm.patrolPath.PathPoints[0];
            }
            
            if (sm.flip * (sm.foeStatesCont.GetState<Patrol<T>>().target.position.x - sm.transform.position.x) < 0)
            {
                sm.flip *= -1;
                var theScale = sm.transform.localScale;
                theScale.z *= -1;
                sm.transform.localScale = theScale;
            }

            sm.ChangeState(sm.foeStatesCont.GetState<Patrol<T>>());
        }
    }
}