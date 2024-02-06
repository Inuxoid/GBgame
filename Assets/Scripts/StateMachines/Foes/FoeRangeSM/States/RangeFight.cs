﻿using System;
using Dto;
using StateMachines.Foes.FoeRangeSM;
using Unity.VisualScripting;
using UnityEngine;

namespace StateMachines.FoeSM.States
{
    public class RangeFight<T> : CombatState<T> where T : BaseFoeSm<T>
    {
        private readonly T sm;
        private bool attackRangeStop;

        public RangeFight(T stateMachine) : base("FoeRangeSm Fight", stateMachine)
        {
            sm = stateMachine;
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
            if (sm.IsPlayerInMeleeAttackZone && sm.IsPlayerSeen())
            {
                Punch();
            }    
            else if (sm.IsPlayerInRangeAttackZone && sm.IsPlayerSeen())
            {
                Shoot();
            }
            
        }

        public override void GetDamage(int damage)
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
                TurnAround();
            }
        }

        private void Shoot()
        {
            var curClip = sm.animator.GetCurrentAnimatorClipInfo(0);
            var isFreeAnimator = !sm.animator.GetBool("isPunching") 
                                 && !sm.animator.GetBool("isShooting") 
                                 && curClip[0].clip.name != "Gunplay" 
                                 && curClip[0].clip.name != "Attack" 
                                 && curClip[0].clip.name != "Sword And Shield Slash" 
                                 && curClip[0].clip.name != "Bandit_Attack";

            sm.animator.SetBool("toDown", sm.transform.position.y > sm.player.transform.position.y + 1);
            sm.animator.SetBool("isShooting", isFreeAnimator && attackRangeStop);
            //Debug.Log("Shoot");
        }

        private void Punch()
        {
            var curClip = sm.animator.GetCurrentAnimatorClipInfo(0);
            var isFreeAnimator = !sm.animator.GetBool("isPunching") &&
                                 curClip[0].clip.name != "Sword And Shield Slash" &&
                                 curClip[0].clip.name != "Bandit_Attack";
            
            sm.animator.SetBool("isPunching", isFreeAnimator);
            //Debug.Log("Punch");
        }

        public override void Hit()
        {
            sm.excPoint.SetActive(false);
            sm.playerSm.GetComponent<LiveCycle>().GetDamage(sm.enemyDamage);
        }
        
        public override void RangeHit()
        {
            sm.excPoint.SetActive(false);
            
            GameObject bullet = GameObject.Instantiate(sm.bulletPrefab, sm.bulletSpawner.transform.position, Quaternion.identity);
            
            RangeFoeBullet bulletScript = bullet.GetComponentInChildren<RangeFoeBullet>();
            if (bulletScript != null)
            {
                bulletScript.SetDirection(sm.playerSm.model.transform.position - sm.transform.position);
                bulletScript.SetDamage(sm.enemyRangeDamage);
            }
        }
        

        private void Move()
        {
            var foePosition = sm.transform.position;
            var borderStop = (Math.Abs(sm.leftBorder.transform.position.x - foePosition.x) < 0.5f && sm.flip == -1)
                               || Math.Abs(sm.rightBorder.transform.position.x - foePosition.x) < 0.5f && sm.flip == 1;

            attackRangeStop = Math.Abs(sm.playerSm.model.transform.position.x - foePosition.x) < sm.rangeAttackDistance - 0.1f;
            
            if (attackRangeStop || borderStop)
            {
                sm.rigidbody.velocity = Vector2.zero;
                sm.animator.SetFloat("hSpeed", 0f);
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