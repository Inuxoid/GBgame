using System;
using Dto;
using Let.Foes.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Let.Foes
{
    public class Foe : MonoBehaviour, IFoe
    {
        [Header("Components")]
        [SerializeField] private GameObject player;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private LiveCycle liveCycle;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Animator animator;
        [SerializeField] private UnityEvent<FloatNumberDto> onHpChanged;
        [SerializeField] private UnityEvent onDamaged;
        [SerializeField] private Heart heart;
        [SerializeField] private ScoreCounter scoreCounter;
        [SerializeField] private GameObject canvas;


        [Header("Settings")]
        [SerializeField] private int enemyDamage;

        [SerializeField] private int maxHealth;
        [SerializeField] private int currentHealth;

        [SerializeField] private float viewDistanceX;
        [SerializeField] private float viewDistanceY;
        [SerializeField] private float maxFightDistanceX;
        [SerializeField] private float maxFightDistanceY;
        [SerializeField] private float minFightDistanceX;
        [SerializeField] private float minFightDistanceY;

        [SerializeField] private float flip;
        [SerializeField] private Vector3 theScale;

        [SerializeField] private float enemySpeed;

        enum FoeState { Idle, Death, Seek, Move, Fight }
        FoeState foeState;
        enum FoeMovementState { MoveTo, MoveFrom, DontMove }
        FoeMovementState foeMovementState;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            player = GameObject.FindGameObjectWithTag("MainPlayer");
            playerController = GameObject.FindGameObjectWithTag("MainPlayer").GetComponent<PlayerController>();
            liveCycle = GameObject.FindGameObjectWithTag("MainPlayer").GetComponent<LiveCycle>();
            animator = GetComponentInChildren<Animator>();
            foeState = FoeState.Seek;
            foeMovementState = FoeMovementState.DontMove;
        }

        private void Update()
        {
            StateMachine();
            animator.SetFloat("hSpeed", Math.Abs(GetComponent<Rigidbody>().velocity.x));
        }

        public void Seek()
        {
            if (Math.Abs(transform.position.y - player.transform.position.y) < viewDistanceY && 
                Math.Abs(transform.position.x - player.transform.position.x) < viewDistanceX)
            {
                foeState = FoeState.Move;
            }
        }

        public void Move()
        {
            switch (foeMovementState)
            {
                case FoeMovementState.MoveTo:
                    //Debug.Log(foeMovementState);
                    MoveTo();
                    break;
                case FoeMovementState.MoveFrom:
                    //Debug.Log(foeMovementState);
                    MoveFrom();
                    break;
                case FoeMovementState.DontMove:
                    //Debug.Log(foeMovementState);
                    DontMove();
                    break;
                default:
                    break;
            }

            void MoveTo()
            {
                if (Math.Abs(transform.position.x - player.transform.position.x) < maxFightDistanceX)
                {
                    foeMovementState = FoeMovementState.DontMove;
                }

                if (flip * (player.transform.position.x - this.transform.position.x) < 0)
                {
                    flip *= -1;
                    theScale = transform.localScale;
                    theScale.x *= -1;
                    transform.localScale = theScale;
                }

                rb.velocity = new Vector2(enemySpeed * new Vector3(player.transform.position.x - transform.position.x + flip * rb.velocity.normalized.x, 0).normalized.x, rb.velocity.y);
            }
        
            void MoveFrom()
            {
                if (Math.Abs(transform.position.x - player.transform.position.x) > minFightDistanceX * 2)
                {
                    foeMovementState = FoeMovementState.DontMove;
                }

                rb.velocity = new Vector2(enemySpeed * new Vector3(transform.position.x - player.transform.position.x, 0).normalized.x, rb.velocity.y);
            }

            void DontMove()
            {
                if (flip * (player.transform.position.x - this.transform.position.x) < 0)
                {
                    animator.Play("Sword And Shield 180 Turn");
                    flip *= -1;
                    theScale = transform.localScale;
                    theScale.x *= -1;
                    transform.localScale = theScale;
                }

                if (Math.Abs(transform.position.x - player.transform.position.x) < maxFightDistanceX &&
                    Math.Abs(transform.position.x - player.transform.position.x) > minFightDistanceX &&
                    Math.Abs(transform.position.y - player.transform.position.y) < maxFightDistanceY)
                {
                    rb.velocity = Vector3.zero;
                    rb.isKinematic = true;
                    foeState = FoeState.Fight;
                }
                else if (Math.Abs(transform.position.x - player.transform.position.x) < minFightDistanceX &&
                         Math.Abs(transform.position.y - player.transform.position.y) < maxFightDistanceY)
                {
                    rb.isKinematic = false;
                    foeMovementState = FoeMovementState.MoveFrom;
                }
                else if (Math.Abs(transform.position.x - player.transform.position.x) > maxFightDistanceX)
                {
                    rb.isKinematic = false;
                    foeMovementState= FoeMovementState.MoveTo;
                }          
            }
        }

        public void Fight()
        {
            if (Math.Abs(transform.position.y - player.transform.position.y) < maxFightDistanceY &&
                Math.Abs(transform.position.x - player.transform.position.x) < maxFightDistanceX &&
                Math.Abs(transform.position.x - player.transform.position.x) > minFightDistanceX)
            {
                Punch();
            }
            else
            {
                foeState = FoeState.Seek;
            }

            void Punch()
            {
                /*if (playerController.IsDead)
                {
                    foeState = FoeState.Idle;
                }
                else if (!animator.GetBool("isPunching"))
                {
                    var curClip = animator.GetCurrentAnimatorClipInfo(0);
                    if (curClip[0].clip.name != "Sword And Shield Slash" && curClip[0].clip.name != "Bandit_Attack")
                    {
                        animator.SetBool("isPunching", true);
                    }   
                }*/
            }
        }

        public void DefaultBehaviourState()
        {
            throw new NotImplementedException();
        }

        public void SeekState()
        {
            throw new NotImplementedException();
        }

        public void FightState()
        {
            throw new NotImplementedException();
        }

        public void StateMachine()
        {
            switch (foeState)
            {
                case FoeState.Idle:
                    //Debug.Log(foeState);
                    break;
                case FoeState.Death:
                    //Debug.Log(foeState);
                    break;
                case FoeState.Seek:
                    //Debug.Log(foeState);
                    Seek();
                    break;
                case FoeState.Move:
                    //Debug.Log(foeState);
                    Move();
                    break;
                case FoeState.Fight:
                    //Debug.Log(foeState);
                    Fight();
                    break;
                default:
                    break;
            }
        }

        public void GetDamage()
        {
            throw new NotImplementedException();
        }

        public void PostDamage()
        {
            liveCycle.GetDamage(enemyDamage);
        }

        public void GetDamage(int damage)
        {
            animator.SetBool("isHited", true);
            currentHealth -= damage;
            FloatNumberDto dto = new FloatNumberDto() { value = currentHealth / maxHealth };
            onHpChanged?.Invoke(dto);
            onDamaged?.Invoke();
            if (currentHealth <= 0 && !animator.GetBool("isDead"))
            {
                animator.SetBool("isDead", true);
                Instantiate(heart, transform.position, Quaternion.identity);
                Death();
            }
        }

        public void Death()
        {
            foeState = FoeState.Death;
            scoreCounter.CountScore(300);
            GetComponent<Collider>().transform.gameObject.layer = LayerMask.NameToLayer("dead");
            canvas.SetActive(false);
        }
    }
}
