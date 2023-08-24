using Dto;
using StateMachines.Foes.FoeShieldSM;
using UnityEngine;
using StateMachines.FoeSM.States;

namespace StateMachines.PlayerSM.States
{
    public class Attack : BaseState
    {
        private PlayerSM sm;

        public Attack(PlayerSM stateMachine) : base("Attack", stateMachine)
        {
            sm = ((PlayerSM)stateMachine);
        }

        public override void Enter()
        {
            base.Enter();
            if (sm.CurStamina > sm.punchStaminaCost)
            {
                //Debug.LogError("if");
                sm.animator.SetBool("isPunching", true);
                sm.CurStamina -= sm.punchStaminaCost;
                FloatNumberDto dto = new FloatNumberDto() { value = sm.CurStamina / sm.maxStamina };
                sm.onStaminaChanged?.Invoke(dto);
            }
            else
            {
                //Debug.LogError("else");
                sm.animator.SetBool("isPunchingWeak", true);
                sm.CurStamina = 0;
                FloatNumberDto dto = new FloatNumberDto() { value = sm.CurStamina / sm.maxStamina };
                sm.onStaminaChanged?.Invoke(dto);
            }
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
            if (sm.animator.GetBool("isPunching") == false && sm.animator.GetBool("isPunchingWeak") == false)
            {
                sm.ChangeState(sm.IdleState);
            }
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public void Hit()
        {
            foreach (var item in Physics.OverlapBox(new Vector3(sm.transform.position.x + (float)sm.flip / 3, sm.transform.position.y, sm.transform.position.z), 
                         new Vector3 (sm.attackRange, sm.attackRange, sm.attackRange), 
                         Quaternion.identity))
            {
                float damage = sm.damageAA * sm.damageMult;

                if (Random.value < sm.critRate)
                {
                    damage *= sm.critValue; 
                }
                
                item?.GetComponent<FoeSM.FoeSM>()?.GetCombatState().GetDamage((int)damage);
                item?.GetComponent<FoeRangeSM.FoeRangeSM>()?.GetCombatState().GetDamage((int)damage);
                item?.GetComponent<FoeShieldSM>()?.GetCombatState().GetDamage((int)damage);
                item?.GetComponent<Turret>()?.GetStrike((int)damage);
                item?.GetComponent<Cyborg>()?.GetStrike((int)damage);
                //Debug.Log(sm.damageAA);
            }
            //sm.animator.SetBool("isPunching", false);
        }
        
        public void WeakHit()
        {
            foreach (var item in Physics.OverlapBox(new Vector3(sm.transform.position.x + (float)sm.flip / 3, sm.transform.position.y, sm.transform.position.z), 
                         new Vector3 (sm.attackRange, sm.attackRange, sm.attackRange), 
                         Quaternion.identity))
            {
                float damage = sm.damageAA;

                if (Random.value < sm.critRate)
                {
                    damage *= sm.critValue; 
                }
                
                item?.GetComponent<FoeSM.FoeSM>()?.GetCombatState().GetDamage((int)damage);
                item?.GetComponent<FoeRangeSM.FoeRangeSM>()?.GetCombatState().GetDamage((int)damage);
                item?.GetComponent<FoeShieldSM>()?.GetCombatState().GetDamage((int)damage);
                item?.GetComponent<Turret>()?.GetStrike((int)damage);
                item?.GetComponent<Cyborg>()?.GetStrike((int)damage);
                //Debug.Log(sm.damageAAWeak);
            }
            //sm.animator.SetBool("isPunching", false);
        }
    }   
}