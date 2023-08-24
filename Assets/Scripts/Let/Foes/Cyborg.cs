using Dto;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cyborg : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;
    [SerializeField] private SpawnEffect effect;
    [SerializeField] private GameObject effectGO;

    [Header("Settings")]
    [SerializeField] private float maxHP;
    [SerializeField] private float hp;
    [SerializeField] private int enemyDamage;
    [SerializeField] private bool canSeePlayer;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstructionMask;
    [SerializeField] private float radius;
    [SerializeField] private float attackSpeed;
    [SerializeField] private int flip = 1;
    [SerializeField] private float currentStrikeTimer;
    [SerializeField] private float maxStrikeTimer;
    [SerializeField] private int enemySpeed;
    [SerializeField] private bool strikesNow;
    [SerializeField] private float yRange;
    [SerializeField] private float xRange;
    [SerializeField] private UnityEvent<FloatNumberDto> onHpChanged;
    [SerializeField] private UnityEvent onPunch;
    [SerializeField] private UnityEvent onTP;
    [SerializeField] private bool tpNow;
    bool needRun;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("MainPlayer");
    }

    public bool CanSeePlayer
    {
        get => canSeePlayer;
        set
        {
            canSeePlayer = value;
            Controller();
        }
    }

    public void GetStrike(int damage)
    {
        hp -= damage;
        FloatNumberDto dto = new FloatNumberDto() { value = this.hp / this.maxHP };
        this.onHpChanged?.Invoke(dto);
        if (hp <= 0)
        {
            Death();
        }
    }

    public void Flip()
    {
        flip = flip * -1;
        Vector3 theScale = this.transform.localScale;
        theScale.x *= -1;
        this.transform.localScale = theScale;
    }

    public void Strike()
    {
        StartCoroutine(StrikeTimer());
    }

    public void Teleport()
    {
        StartCoroutine(TpTimer(player.transform, false));
    }

    public void MoveToPlayer()
    {
        if (flip * (player.transform.position.x - this.transform.position.x) < 0)
        {
            Flip();
        }
        //Debug.Log(Math.Abs(player.transform.position.y - this.transform.position.y));
        if (Math.Abs(player.transform.position.y - this.transform.position.y) > yRange && player.GetComponent<PlayerMovement>().IsGrounded)
        {
            Teleport();
        }

        //this.transform.Translate((player.transform.position - this.transform.position) * 0.02f, Space.World);
        if (Math.Abs(player.transform.position.x - transform.position.x) > xRange)
        {
            needRun = true;
            animator.SetBool("isPunching", false);
            //rb.AddForce((new Vector3((player.transform.position.x - transform.position.x), 
            //    transform.position.y, transform.position.z).normalized) * enemySpeed, ForceMode.Force);
        }
        else
        {
            needRun = false;
            rb.velocity = Vector3.zero;
        }
    }

    public void Controller()
    {
        if (CanSeePlayer)
        {
            MoveToPlayer();
        }
        else
        {
            //transform.position = placed.transform.position;
            rb.velocity = Vector3.zero;
            //transform.position = Vector3.MoveTowards(transform.position, placed.transform.position, Time.deltaTime);
        }
    }

    public void Death()
    {

        Destroy(this.gameObject);
    }

    private void Start()
    {
        StartCoroutine(CheckPLayer());
        StartCoroutine(TpTimer(transform, true));
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !strikesNow && !tpNow)
        {
            Strike();
            strikesNow = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        strikesNow = false;
    }

    private IEnumerator StrikeTimer()
    {
        while (true)
        {
            if (Math.Abs(rb.velocity.x) < 0.2f && Math.Abs(player.transform.position.x - transform.position.x) < xRange)
            {
                animator.SetBool("isPunching", true);
                foreach (var item in Physics.OverlapBox(new Vector3(this.transform.position.x + flip, this.transform.position.y),
                                        new Vector3(1, 1, 1),
                                        Quaternion.identity, 8))
                {
                    item.GetComponentInParent<LiveCycle>()?.GetDamage(enemyDamage);
                    onPunch?.Invoke();
                    break;
                }
                yield return new WaitForSeconds(attackSpeed);
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void Update()
    {
        animator.SetFloat("hSpeed", Math.Abs(GetComponent<Rigidbody>().velocity.x));
        if (tpNow)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        else if (needRun)
        {
            RunTo();
        }
    }

    public void RunTo()
    {
        if (flip * (player.transform.position.x - this.transform.position.x) < 0)
        {
            Flip();
        }
        rb.velocity = new Vector2(enemySpeed * new Vector3(player.transform.position.x - transform.position.x, 0).normalized.x, rb.velocity.y);
    }

    private IEnumerator CheckPLayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
            
            if (rangeChecks.Length != 0 && !tpNow)
            {
                Transform target = rangeChecks[0].transform;

                Vector3 directionToTarget = (target.position - transform.position).normalized;
                RaycastHit hit;
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                //Debug.Log($"{Physics.Raycast(transform.position, directionToTarget, out hit, distanceToTarget * 2f, obstructionMask)} + {hit.collider}");
                if (Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    CanSeePlayer = true;
                    //Debug.Log("Can see u");
                }
                else
                {
                    CanSeePlayer = false;
                }

            }
            else if (CanSeePlayer)
                CanSeePlayer = false;
        }
    }

    private IEnumerator TpTimer(Transform newTransform, bool start)
    {
        onTP?.Invoke();
        tpNow = true;
        strikesNow = false;
        if (!start)
        {
            this.transform.position = newTransform.position + new Vector3(1, -1, 0);
        }
        MoveToPlayer();
        animator.SetBool("isPunching", false);
        effectGO.SetActive(true);
        effect.PlaySp();
        yield return new WaitForSeconds(1f);
        effect.StopSP();
        effectGO.SetActive(false);
        tpNow = false;
        yield return null;
    }
}
