using System;
using System.Collections;
using System.Collections.Generic;
using Dto;
using StateMachines.PlayerSM.States;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using TMPro;
using UI;
using Unity.VisualScripting;
using UnityEngine.Serialization;

namespace StateMachines.PlayerSM
{
    public class PlayerSM : StateMachines.StateMachine
    {
        [Header("States")]
        public Idle IdleState;
        public Run RunState;
        public Jump JumpState;
        public JumpForward JumpForwardState;
        public Crouch CrouchState;
        public Climb ClimbState;
        public HideIdle HideIdleState;
        public HideCrouch HideCrouchState;
        public Impact ImpactState;
        public Attack AttackState;
        public Falling FallingState;
        public Dead DeadState;
        public StrongPunchState StrongPunchState;

        [Header("Components")] 
        public FoeSM.FoeSM[] foes;
        public Outline outline;
        public Color outlineColor;
        public LiveCycle liveCycle;
        public Rigidbody rb;
        public Animator animator;
        public PlayerInput playerInput;
        public CustomGravity customGravity;
        public GroundChecker groundChecker;

        public Collider bodyCollider;
        public Collider visionCollider;

        public Player.Climb climb;
        public GameObject ragdoll;
        public GameObject model;

        [Header("Sounds")] 
        public UnityEvent onLanding;

        [Header("Settings")] 
        
        public UnityEvent<FloatNumberDto> onStaminaChanged;
        
        public bool detected;
        
        public Vector3 halfExtents = new Vector3(0.5f, 0.8f, 0.8f);
        
        public float cachedInput;
        public float maxDistanceToStop = 0.5f;
        public bool canMoveAfterFalling = true;
        
        public bool isJumpButtonWasPressed;
        public bool isJumpButtonIsPressed;
        
        public float currentFallingHeight;
        public float maxFallingHeight;
        public float deathFallingHeight = 15f;

        public float hardStopNeededSpeed;
    
        public bool canAction;
        
        public float runSpeed = 40;
        public float crouchSpeed = 15;
        public float hideSpeed = 10;
        
        public int flip = 1;
        public bool facingRight = true;
        
        public bool canHide;
        public bool willDead;

        public float CurStamina
        {
            get => curStamina;
            set
            {
                if (value >= 0 && value <= maxStamina)
                {
                    curStamina = value;
                }
                else if (value < 0)
                {
                    curStamina = 0;
                }
                else
                {
                    curStamina = maxStamina;
                }
            }
        }

        public float curStamina = 100;
        public float maxStamina = 100;
        public float punchStaminaCost = 40;
        public float staminaRes = 1;
        public float staminaResTimer = 0.5f;
        public float unHidingSpeed = 1f;
        public float hidingSpeed = 1f;
        public bool underAttack = false;
        
        public bool CanHide
        {
            get => canHide && canHidePause && !underAttack;
            set => canHide = value;
        }

        public bool canHidePause = true;
        public bool canUnHide;
        
        public bool CanUnHide
        {
            get => canUnHide && canUnHidePause;
            set => canUnHide = value;
        }
        
        public bool canUnHidePause = true;
        public bool isHidden;

        public bool IsHidden
        {
            get => isHidden;
            set
            {
                if (value)
                {
                    canUnHidePause = false;
                    StartCoroutine(UnHideTimer());
                }
                else
                {
                    canHidePause = false;
                    StartCoroutine(HideTimer());
                }
                isHidden = value;
            }
        }

        public int damageAA;
        public float damageMult = 1;
        public int damageAAWeak = 10;
        public float critValue = 1f;
        public float critRate = 0f;
        public float attackRange = 0.7f;
        
        public float jumpHeight = 0.8f;
        public float jumpForwardHeight = 0.8f;
        public float jumpForwardForceForward = 60f;
        public float jumpForwardForceForwardMult = 1;
        
        public float fallingXSpeed = 0.8f;

        public bool isGrounded;
        public float standDistance = 1.3f;
        public Vector3 curCast= new Vector3(0.3f, 1.44f, 0.7f);

        public bool canStandUp = true;
        
        public GameObject[] katanaModels;

        private void Awake()
        {
            IdleState = new Idle(this);
            RunState = new Run(this);
            JumpState = new Jump(this);
            JumpForwardState = new JumpForward(this);
            CrouchState = new Crouch(this);
            ClimbState = new Climb(this);
            HideIdleState = new HideIdle(this);
            HideCrouchState = new HideCrouch(this);
            // ImpactState = new Impact(this);
            AttackState = new Attack(this);
            FallingState = new Falling(this);
            DeadState = new Dead(this);
            StrongPunchState = new StrongPunchState(this);
            StartCoroutine(StaminaRes());

            // Find components
            foes = FindObjectsOfType<FoeSM.FoeSM>();

            outline = GetComponentInChildren<Outline>();
            outlineColor = new Color(112, 214, 150, 255);
            liveCycle = GetComponent<LiveCycle>();
            rb = GetComponent<Rigidbody>();
            animator = GetComponentInChildren<Animator>();
            playerInput = GetComponent<PlayerInput>();
            customGravity = GetComponent<CustomGravity>();
            groundChecker = GetComponentInChildren<GroundChecker>();
            bodyCollider = GetComponentInChildren<Collider>();
            visionCollider = GetComponentInChildren<Collider>();
            climb = GetComponentInChildren<Player.Climb>();

            var buttonIconUpdater = FindObjectOfType<ActionButton>();
                if (buttonIconUpdater != null)
                {
                    AddStateObserver(buttonIconUpdater);
                }
        }
        
        public void UpdateKatanaModel()
        {
            katanaModels[0].SetActive(!katanaModels[0].activeInHierarchy);
            katanaModels[1].SetActive(!katanaModels[1].activeInHierarchy);
        }

        public void DeathProc()
        {
            //Debug.Log("I'm dead");
            ragdoll.SetActive(true);
            Instantiate(ragdoll, model.transform.position, model.transform.rotation);
            Destroy(model);
        }
        
        protected override BaseState GetInitialState()
        {
            return IdleState;
        }

        private IEnumerator UnHideTimer()
        {
            yield return new WaitForSeconds(unHidingSpeed);
            canUnHidePause = true;
        }

        private IEnumerator HideTimer()
        {
            yield return new WaitForSeconds(hidingSpeed);
            canHidePause = true;
        }

        private IEnumerator StaminaRes()
        {
            while (CurrentState != DeadState)
            {
                if (CurStamina < maxStamina)
                {
                    float startTime = Time.time;

                    while (CurStamina < maxStamina && Time.time - startTime < staminaResTimer)
                    {
                        CurStamina += staminaRes * Time.deltaTime / staminaResTimer;
                        FloatNumberDto dto = new FloatNumberDto() { value = CurStamina / maxStamina };
                        onStaminaChanged?.Invoke(dto);

                        yield return null; // wait for the next frame
                    }
                }

                yield return new WaitForSeconds(staminaResTimer);
            }
        }
    }
}