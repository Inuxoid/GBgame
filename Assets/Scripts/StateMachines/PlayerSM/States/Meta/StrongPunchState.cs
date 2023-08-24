using System.Collections.Generic;
using Dto;
using Meta.Upgrades.Controller;
using Meta.Upgrades.Model;
using Meta.Upgrades.View.HUD;
using StateMachines.Foes.FoeShieldSM;
using UnityEngine;

namespace StateMachines.PlayerSM.States
{
    public class StrongPunchState : BaseState
    {   
        private PlayerSM sm;
        private float charge;
        private bool isInterrupted;
        private bool isPunched;
        public SocketViewInHUD socketViewInHUD;

        public StrongPunchState(PlayerSM stateMachine) : base("StrongPunchState", stateMachine)
        {
            sm = ((PlayerSM)stateMachine);
        }

        public override void Enter()
        {
            base.Enter();
            isPunched = false;
            sm.animator.SetBool("isPunchingWeak", false);
            sm.animator.SetBool("isStrongPunchingFinish", false);
            sm.animator.SetBool("isStrongPunching", false);
            charge = 0f;
            isInterrupted = false;
            if (sm.CurStamina > sm.punchStaminaCost)
            {
                //Debug.LogError("if");
                sm.animator.SetBool("isStrongPunching", true);
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
            if (sm.animator.GetBool("isStrongPunching") == false && sm.animator.GetBool("isStrongPunchingFinish")  == false)
            {
                sm.ChangeState(sm.IdleState);
            }
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
            float maxChargeTime = UpgradeLib.StrongPunch.curGrade.Item1; // Максимальное время зарядки
            float maxDamageMultiplier = UpgradeLib.StrongPunch.curGrade.Item2; // Максимальный множитель урона
            
            switch (isInterrupted)
            {
                case false when socketViewInHUD.pressedButton.isPressed:
                {
                    float chargeIncreaseRate = maxChargeTime / maxDamageMultiplier; // Величина увеличения заряда в единицу времени
                    charge += Time.deltaTime * chargeIncreaseRate; // Увеличиваем заряд в зависимости от времени

                    // Ограничиваем заряд, чтобы не превысить максимальное значение
                    charge = Mathf.Clamp(charge, 0f, maxChargeTime);
                
                    sm.animator.SetBool("isStrongPunching", true);

                    Debug.Log("Charge: " + charge + " / Damage Multiplier: " + (charge / maxChargeTime * maxDamageMultiplier));
                    Debug.Log("1");
                    break;
                }
                case false when !isPunched && socketViewInHUD.pressedButton.isReleased:
                    sm.animator.SetBool("isStrongPunchingFinish", true);
                    sm.animator.SetBool("isStrongPunching", false);
                    isPunched = true;
                    Debug.Log("2");
                    break;
                default:
                    sm.animator.SetBool("isStrongPunching", false);
                    sm.ChangeState(sm.IdleState); // анимацию прерывания если дойдём
                    Debug.Log("3");
                    break;
            }
        }

        public void Hit()
        {
            foreach (var item in Physics.OverlapBox(new Vector3(sm.transform.position.x + (float)sm.flip / 3, sm.transform.position.y, sm.transform.position.z), 
                         new Vector3 (0.7f, 0.7f, 0.7f), 
                         Quaternion.identity))
            {
                item?.GetComponent<FoeSM.FoeSM>()?.GetCombatState().GetDamage((int)((float)sm.damageAA * charge));
                item?.GetComponent<FoeRangeSM.FoeRangeSM>()?.GetCombatState().GetDamage((int)((float)sm.damageAA * charge));
                item?.GetComponent<FoeShieldSM>()?.GetCombatState().GetDamage((int)((float)sm.damageAA * charge));
                item?.GetComponent<Turret>()?.GetStrike((int)((float)sm.damageAA * charge));
                item?.GetComponent<Cyborg>()?.GetStrike((int)((float)sm.damageAA * charge));
                //Debug.Log(sm.damageAA);
            }
            //sm.animator.SetBool("isPunching", false);
        }
    }
}