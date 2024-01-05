﻿using System;
using Dto;
using Let.Foes;
using Presenters;
using StateMachines.FoeSM.States;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace StateMachines
{
    public abstract class BaseFoeSm<T> : StateMachine where T : BaseFoeSm<T>
    {
        public FoeStatesCont<T> foeStatesCont;
        public GameObject foeEyes;
        public PlayerSM.PlayerSM playerSm;
        [SerializeField] public UnityEvent<FloatNumberDto> onHpChanged;
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
        public float hHearDistance = 1.6f;
        public float vHearDistance = 2.0f;
        public float verticalDetectDistance = 1.6f;
        public float timer;
        public bool isPlayerInFrontOf;
        public Color visionColorInHide;
        public int maxShieldStrength;
        public float rangeAttackDistance = 1.5f;
        public float meleeAttackDistance = 0.7f;
        public TextMeshProUGUI stateTmpro;

        public abstract CombatState<T> GetCombatState();
        public bool IsPlayerDetected
        {
            get
            {
                var detected = IsPlayerSeen();
                if(IsPlayerHeard() && !detected)
                   TurnToPlayer();
                detected = IsPlayerSeen();
                UpdateVisualState(detected);
                return detected;
            }
        }

        public bool IsPlayerInRangeAttackZone => Vector2.Distance(foeEyes.transform.position, playerSm.model.transform.position) < rangeAttackDistance;

        public bool IsPlayerInMeleeAttackZone => Vector2.Distance(foeEyes.transform.position, playerSm.model.transform.position) < meleeAttackDistance;

        protected void Awake()
        {
            foeStatesCont = new FoeStatesCont<T>((T)this);
            onHpChanged.AddListener(transform.Find("HpCanvas").Find("Image").GetComponent<BarPresenter>().DrawFloat);
        }

        private new void LateUpdate()
        {
            base.LateUpdate();
            var animatorinfo = animator.GetCurrentAnimatorClipInfo(0);
            var current_animation = animatorinfo[0].clip.name;
            if (stateTmpro != null)
                stateTmpro.text = $"{CurrentState.name}\n{current_animation}";
        }

        public bool IsPlayerSeen()
        {
            var fromPosition = foeEyes.transform.position;
            var toPosition = player.transform.position;
            var direction = toPosition - fromPosition;
            isPlayerInFrontOf = direction.x * flip > 0;

            if (!isPlayerInFrontOf || Math.Abs(direction.x) > detectDistance || playerSm.isHidden)
            { 
                return false;
            }

            var hits = Physics.RaycastAll(fromPosition, direction, detectDistance);

            // Визуализация рейкаста
            Debug.DrawLine(fromPosition, fromPosition + direction.normalized * detectDistance, Color.red, 0.2f);

            foreach (var hit in hits)
            {
                if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Ground"))
                {
                    return false;
                }

                if (hit.collider.GetComponentInParent<PlayerSM.PlayerSM>())
                {
                    // Визуализация успешного обнаружения игрока
                    Debug.DrawLine(fromPosition, hit.point, Color.green, 0.2f);
                    return true;
                }
            }

            return false;
        }


        public bool IsPlayerHeard()
        {
            var fromPosition = foeEyes.transform.position;
            var toPosition = player.transform.position;
            var direction = toPosition - fromPosition;
            isPlayerInFrontOf = direction.x * flip > 0;
            
            var hDistanceToPlayer = Math.Abs(fromPosition.x - toPosition.x);
            var vDistanceToPlayer = Math.Abs(fromPosition.y - toPosition.y);
            var inDistance = vDistanceToPlayer < vHearDistance && hDistanceToPlayer < hHearDistance;
            var isLoud = playerSm.CurrentState == playerSm.RunState;
            
            if (!inDistance || !isLoud || playerSm.isHidden)
            {
                return false;
            }

            var hits = Physics.RaycastAll(fromPosition, direction, detectDistance);

            foreach (var hit in hits)
            {
                if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Ground"))
                {
                    ChangeState(foeStatesCont.GetState<Listen<T>>());
                    return true;
                }

                if (hit.collider.GetComponentInParent<PlayerSM.PlayerSM>())
                {
                    return true;
                }
            }

            return false;
        }

        
        private void UpdateVisualState(bool isDetected)
        {
            vision.SetActive(!isDetected);
            canvas.SetActive(isDetected);
        }

        private void TurnToPlayer()
        {
            if (isPlayerInFrontOf) return;
            flip *= -1;
            var theScale = transform.localScale;
            theScale.z *= -1;
            transform.localScale = theScale;
            ChangeState(GetCombatState());
        }

        protected void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponentInParent<PlayerSM.PlayerSM>() && !IsPlayerSeen())
            {
                //Debug.LogError($"{IsPlayerSeen()} {CurrentState}");
                //TurnToPlayer();
            }
        }
    }
}