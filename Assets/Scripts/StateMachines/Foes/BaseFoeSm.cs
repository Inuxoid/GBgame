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

        protected void Awake()
        {
            foeStatesCont = new FoeStatesCont<T>((T)this);
        }

        public float rangeAttackDistance = 1.5f;
        public float meleeAttackDistance = 0.7f;
        //public new RangeFight FightState; 

        public bool IsPlayerInRangeAttackZone
        {
            get
            {
                //Debug.Log($"Distance {Vector2.Distance(transform.position, player.transform.position)} < rangeAttackDistance {rangeAttackDistance}");
                return Vector2.Distance(foe.transform.position, playerSm.model.transform.position) < rangeAttackDistance;
            }
        }

        public bool IsPlayerInMeleeAttackZone
        {
            get
            {
                //Debug.Log($"Distance {Vector2.Distance(foe.transform.position, player.transform.position)} < meleeAttackDistance {meleeAttackDistance}");
                return Vector2.Distance(foe.transform.position, playerSm.model.transform.position) < meleeAttackDistance;
            }
        }


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
        public Transform lastViewedPlayerPosition;
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
        
        
        protected bool isPlayerDetectedCached;
        public bool IsPlayerDetected => IsDetected();

        protected bool IsDetected()
        {
            RaycastHit hit;
            var fromPosition = foe.transform.position;
            var toPosition = player.transform.position;
            var direction = toPosition - fromPosition;


            isPlayerInFrontOf = direction.x * flip > 0;
            var isDetected = false;
            vision.SetActive(true);
            canvas.SetActive(false);
            //Debug.Log($"По X {Math.Abs(direction.x)} < {detectDistance} По Y {direction.y} < 3");
            //Debug.Log(Physics.Raycast(transform.position,direction,out hit));
            //Debug.Log($"if({Physics.Raycast(foe.transform.position,direction,out hit)} && {isPlayerInFrontOf} && {Math.Abs(direction.x) < detectDistance} && {!playerSm.isHidden} && {Math.Abs(direction.y) < 1.5f})");
            if(Physics.Raycast(foe.transform.position,direction,out hit)
               && isPlayerInFrontOf 
               && Math.Abs(direction.x) < detectDistance 
               && !playerSm.isHidden
               && Math.Abs(direction.y) < 1.5f)
            {
                //Debug.Log("Внутри124");
                if (hit.collider.GetComponentInParent<PlayerSM.PlayerSM>())
                {
                    //Debug.Log("Внутри124512412421");
                    isDetected = true;
                    vision.SetActive(false);
                    canvas.SetActive(true);
                }
                else
                {
                    //Debug.LogError(hit.collider);
                }

            }
            if (!isDetected)
            {
                lastViewedPlayerPosition = player.transform;
                //Debug.Log("Не детект");
            }
            else
            {
               // Debug.Log("Детект");
            }

            isPlayerDetectedCached = isDetected;        
            
            return isDetected;
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

        public abstract CombatState<T> GetCombatState();
    }
}