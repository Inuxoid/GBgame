using System;
using System.Collections;
using Dto;
using StateMachines.FoeSM.States;
using UnityEngine;

namespace StateMachines.Foes.FoeShieldSM.States
{
    public class ShieldFight<T> : CombatState<T> where T : BaseFoeSm<T>
    {
        private readonly T sm;
        private bool attackRangeStop;
        private readonly Shield shield;
        private float turnAroundDelay = 1f;
        
        public ShieldFight(T stateMachine) : base("FoeShieldSm Fight", stateMachine)
        {
            sm = stateMachine;
            shield = new Shield(sm.maxShieldStrength);
        }

        public override void Enter()                                                 
        {                                                                            
            base.Enter();                          
            sm.excPoint.SetActive(true);
            sm.playerSm.underAttack = true;
        }                                                                            
                                                                             
        public override void UpdateLogic()                                           
        {                                                                            
            base.UpdateLogic();                                                      
                                                                             
            if (!sm.IsPlayerDetected)                              
            {                                                                        
                sm.ChangeState(sm.foeStatesCont.GetState<Seek<T>>());
            }                                                                        
                                                                             
            if (sm.currentHealth <= 0)                        
            {                                                                        
                sm.ChangeState(sm.foeStatesCont.GetState<Death<T>>());
            }                                                                        
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
            if (sm.playerSm.CurrentState == sm.playerSm.DeadState) return;
            Move();
            Punch();
        }

        public override void GetDamage(int damage)
        {
            if (!shield.IsShieldBroken && sm.isPlayerInFrontOf)
            {
                sm.animator.SetBool("isHited", true);
                shield.TakeDamage(damage);
                FloatNumberDto dto = new FloatNumberDto() { value = shield.CurrentShieldStrength / shield.MaxShieldStrength };
                sm.onShieldHpChanged?.Invoke(dto);
                if (shield.IsShieldBroken)
                {
                    sm.shield.SetActive(false);
                    sm.animator.SetBool("isShieldBroken", true);
                }
            }
            else
            {
                sm.excPoint.SetActive(false);
                sm.animator.SetBool("isPunching", false);
                sm.animator.SetBool("isShooting", false);
                sm.animator.SetBool("isHited", true);
                sm.currentHealth -= damage;
                FloatNumberDto dto = new FloatNumberDto() { value = sm.currentHealth / sm.maxHealth };
                sm.onHpChanged?.Invoke(dto);
                if (!sm.isPlayerInFrontOf)
                {
                    sm.StartCoroutine(DelayedTurnAround());
                }
            }
        }

        
        private void Punch()
        {
            var curClip = sm.animator.GetCurrentAnimatorClipInfo(0);
            var isFreeAnimator = !sm.animator.GetBool("isPunching")
                                 && curClip[0].clip.name != "Attack" 
                                 && curClip[0].clip.name != "Sword And Shield Slash" 
                                 && curClip[0].clip.name != "Bandit_Attack";
            
            sm.animator.SetBool("isPunching", attackRangeStop && isFreeAnimator);
            //Debug.Log($"{sm.IsPlayerInMeleeAttackZone} && {isFreeAnimator}");
        }

        public override void Hit()
        {
            sm.excPoint.SetActive(false);
            sm.playerSm.GetComponent<LiveCycle>().GetDamage(sm.enemyDamage);
        }

        public override void RangeHit()
        {
            throw new NotImplementedException();
        }


        private void Move()
        {
            var foePosition = sm.transform.position;
            var borderStop = (Math.Abs(sm.leftBorder.transform.position.x - foePosition.x) < 0.5f && sm.flip == -1)
                               || Math.Abs(sm.rightBorder.transform.position.x - foePosition.x) < 0.5f && sm.flip == 1;

            attackRangeStop = Math.Abs(sm.playerSm.model.transform.position.x - foePosition.x) <= sm.meleeAttackDistance - 0.1f;
            
            //Debug.Log(sm.playerSm.transform.position.x - foePosition.x);
            
            if (attackRangeStop || borderStop)
            {
                sm.rigidbody.velocity = Vector2.zero;
                sm.animator.SetFloat("hSpeed", 0f);
                Debug.LogError("Stopped");
                return;
            }
            

            
            sm.animator.SetFloat("hSpeed", 1f);
            sm.animator.SetBool("isPunching", false);
            sm.rigidbody.velocity = new Vector2(sm.enemyFightSpeed * new Vector2
            (
                sm.playerSm.model.transform.position.x 
                - foePosition.x, 0
            ).normalized.x, 0);

            if (sm.flip * (sm.playerSm.model.transform.position.x - foePosition.x) < 0)
                TurnAround();
        }
        
        private IEnumerator DelayedTurnAround()
        {
            yield return new WaitForSeconds(turnAroundDelay);
            TurnAround();
        }
        
        private void TurnAround()
        {
            sm.flip *= -1;
            var transform = sm.transform;
            var theScale = transform.localScale;
            theScale.z *= -1;
            transform.localScale = theScale;
        }

        public override void Exit()
        {
            sm.playerSm.underAttack = false;
            base.Exit();
        }
    }
}