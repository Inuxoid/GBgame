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
            

            
            if (sm.patrolPath.PathPoints.Length == 2) // Check if foe has route or just staying
            { 
                var position = sm.transform.position;
                var distanceToPoint1 = Vector2.Distance(position, sm.leftBorder.transform.position);
                var distanceToPoint2 = Vector2.Distance(position, sm.rightBorder.transform.position);


                // Set first point to move for patrol - the most close border to player
                sm.foeStatesCont.GetState<Patrol<T>>().target = 
                    distanceToPoint1 < distanceToPoint2 
                        ? sm.leftBorder.transform 
                        : sm.rightBorder.transform;
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