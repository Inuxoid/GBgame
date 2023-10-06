using System;
using Dto;
using Let.Foes;
using UnityEngine;
using UnityEngine.Events;

namespace StateMachines
{
    public abstract class BaseFoeSm<T> : StateMachine where T : BaseFoeSm<T>
    {
        public FoeStatesCont<T> foeStatesCont;
        public GameObject foe;
        public PlayerSM.PlayerSM playerSm;
        public UnityEvent<FloatNumberDto> onHpChanged;
        public UnityEvent<FloatNumberDto> onShieldHpChanged;
        public PatrolPath patrolPath;
        public Rigidbody rigidbody;
        public GameObject player;
        public Animator animator;
        public ScoreCounter scoreCounter;
        public GameObject canvas;
        public GameObject vision;
        public GameObject excPoint;
        public GameObject shield;
        public GameObject leftBorder;
        public GameObject rightBorder;
        public float currentHealth;
        public float maxHealth;
        public int flip;
        public float enemySpeed;
        public float enemyFightSpeed;
        public int enemyDamage;
        public int enemyRangeDamage;
        public int detectDistance;
        public float timer;
        public bool isPlayerInFrontOf;
        public Color visionColorInHide;
        public int maxShieldStrength;
        public float rangeAttackDistance = 1.5f;
        public float meleeAttackDistance = 0.7f;
        
        public abstract CombatState<T> GetCombatState();
        public bool IsPlayerDetected => IsDetected();
        public bool IsPlayerInRangeAttackZone => Vector2.Distance(foe.transform.position, playerSm.model.transform.position) < rangeAttackDistance;

        public bool IsPlayerInMeleeAttackZone => Vector2.Distance(foe.transform.position, playerSm.model.transform.position) < meleeAttackDistance;

        protected void Awake()
        {
            foeStatesCont = new FoeStatesCont<T>((T)this);
        }
        private bool IsDetected()
        {
            var fromPosition = foe.transform.position;
            var toPosition = player.transform.position;
            var direction = toPosition - fromPosition;
            isPlayerInFrontOf = direction.x * flip > 0;
            vision.SetActive(true);
            canvas.SetActive(false);
            if (!Physics.Raycast(foe.transform.position, direction, out var hit)
                || !isPlayerInFrontOf
                || !(Math.Abs(direction.x) < detectDistance)
                || playerSm.isHidden
                || !(Math.Abs(direction.y) < 1.5f)) return false;
            if (!hit.collider.GetComponentInParent<PlayerSM.PlayerSM>()) return false;
            vision.SetActive(false);
            canvas.SetActive(true);
            return true;
        }

        protected void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponentInParent<PlayerSM.PlayerSM>() && CurrentState != GetCombatState())
            {
                flip *= -1;
                var theScale = transform.localScale;
                theScale.z *= -1;
                transform.localScale = theScale;
                ChangeState(GetCombatState());
            }
        }
    }
}