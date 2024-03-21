using System;
using System.Linq;
using UnityEngine;

namespace StateMachines.PlayerSM.States
{
    public class Somersault : BaseState
    {
        private PlayerSM sm;
        private int originalLayer; // Для сохранения изначального слоя игрока
        private int rollLayer; // Слой для кувырка
        private float lastRollTime; // Время с последнего кувырка
        private readonly float rollCooldown = 0.5f; // КД кувырка
        
        public Somersault(PlayerSM stateMachine) : base("Somersault", stateMachine)
        {
            sm = stateMachine;
        }

        public override void Enter()
        {
            base.Enter();
            sm.canMoveAfterFalling = true;
            if (CheckAvailability())
            {
                rollLayer = LayerMask.NameToLayer("RollLayer");
                originalLayer = sm.gameObject.layer;
                sm.gameObject.layer = rollLayer;
                PerformRoll();
                
                lastRollTime = Time.time; // Обновляем время последнего кувырка
            }
            else
            {
                sm.ChangeState(sm.IdleState);
            }
        }

        private bool CheckAvailability()
        {
            bool isCooldownComplete = (Time.time - lastRollTime) >= rollCooldown; // Проверка кулдауна
            bool isEnoughStamina = sm.curStamina >= sm.rollStaminaCost;
            bool canRoll = isCooldownComplete && isEnoughStamina;
            
            if (canRoll)
            {
                bool raycast = Physics.BoxCast(sm.transform.position, sm.halfExtents, 
                    new Vector3(sm.flip, 0f, 0f), out var hit, Quaternion.identity, sm.rollDistance);
                if (raycast)
                {
                    bool compareTag = hit.collider.CompareTag("Wall");
                    canRoll = !compareTag;
                }
            }
            
            return canRoll;
        }

        private bool CheckCeil()
        {
            bool toCrouch = false;
            bool raycast = Physics.BoxCast(sm.transform.position, sm.halfExtents, 
                new Vector3(sm.flip, 0f, 0f), out var hit, Quaternion.identity, sm.rollDistance);
            if (raycast)
            {
                bool compareTag = hit.collider.CompareTag("Ceil");
                toCrouch = !compareTag;
            }

            return toCrouch;
        }
        
        private void PerformRoll()
        {
            // Запуск анимации кувырка
            sm.animator.Play("RollAnimation");

            // Перемещение игрока вперед. 
            sm.transform.Translate(sm.rollDistance, 0, 0);

            // Отнимаем стамину
            ReduceStamina(sm.rollStaminaCost);
            
                sm.ChangeState(sm.CrouchState);

        }
        
        private void ReduceStamina(float amount)
        {
            sm.curStamina -= amount;
            // Здесь может быть код для обновления UI стамины и т.д.
        }
        
        public override void UpdateLogic()
        {
            base.UpdateLogic();
            CheckClimb();
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics(); 
        }

        public override void Exit()
        {
            // Возвращаем игрока на его изначальный слой
            sm.gameObject.layer = originalLayer;
            base.Exit();
        }
        
        private void CheckClimb()
        {
            var centerVector = new Vector3(
                sm.transform.position.x + (float)sm.flip / 3,
                sm.transform.position.y - 0.18f,
                sm.transform.position.z);

            var colliders = Physics.OverlapBox(centerVector, sm.curCast / 2)
                .Where(x => (x.GetComponent<ClimbData>() != null || x.CompareTag("Wall"))
                            && Math.Abs(x.transform.position.x - sm.transform.position.x) > 0.6f).ToList();

            if (colliders.Count == 0) return;
            
            foreach (var item in colliders)
            {
                if (!item.CompareTag("Ground")) continue;
                
                if (!(sm.playerInput.actions["Move"].ReadValue<Vector2>().x *
                        (item.transform.position.x - sm.transform.position.x) > 0)) continue;
                
                var left = !((item.transform.position.x - sm.transform.position.x) < 0);
                
                if (!item.TryGetComponent(out ClimbData climbData) ||
                    !sm.climb.IsCanClimb(climbData.FirstPoint(left))) continue;
                
                sm.ClimbState.points = climbData.Points(left);
                
                sm.ChangeState(sm.ClimbState);
                return;
            }
        }
    }
}