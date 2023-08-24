using Dto;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject placed;
    [SerializeField] private GameObject heart;
    [SerializeField] private Animator animator;
    [SerializeField] private ScoreCounter scoreCounter;
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject canvas;

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
    [SerializeField] private bool needRun;
    [SerializeField] private float xRange;
    [SerializeField] private UnityEvent<FloatNumberDto> onHpChanged;
    [SerializeField] private UnityEvent onPunch;
    [SerializeField] private bool dead;

    public bool CanSeePlayer { get => canSeePlayer;
        set
        {
            canSeePlayer = value;
            Controller();
        }
    }

    public void GetStrike(int damage)
    {
        animator.SetBool("isHited", true);
        animator.SetBool("isPunching", false);
        hp -= damage;
        FloatNumberDto dto = new FloatNumberDto() { value = this.hp / this.maxHP };
        this.onHpChanged?.Invoke(dto);
        if (hp <= 0 && !dead)
        {
            Instantiate(heart, this.transform.position, Quaternion.identity);
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

    public void MoveToPlayer()
    {
        //this.transform.Translate((player.transform.position - this.transform.position) * 0.02f, Space.World);
        //Debug.Log(Math.Abs(player.transform.position.x - transform.position.x));
        if (Math.Abs(player.transform.position.x - transform.position.x) > xRange && !dead)
        {
            //rb.AddForce((new Vector3((player.transform.position.x - transform.position.x),
            //    0, transform.position.z).normalized) * enemySpeed, ForceMode.Force);
            needRun = true;
            animator.SetBool("isPunching", false);
        }
        else
        {
            //Debug.Log("Zero1");
            needRun = false;
            rb.velocity = Vector3.zero;
        }
    }

    public void RunTo()
    {
        if (flip * (player.transform.position.x - this.transform.position.x) < 0 && !dead )
        {
            Flip();
        }
        if (needRun && !dead )
            rb.velocity = new Vector2(enemySpeed * new Vector3(player.transform.position.x - transform.position.x, 0).normalized.x, rb.velocity.y);
    }

    public void Controller()
    {
        if (CanSeePlayer && !dead)
        {
            MoveToPlayer();
        }
        else
        {
            //transform.position = placed.transform.position;
            rb.velocity = Vector3.zero;
            //Debug.Log("Zero2");
            //transform.position = Vector3.MoveTowards(transform.position, placed.transform.position, Time.deltaTime);
        }
    }

    public void Death()
    {
        if (!dead)
        {
            animator.SetBool("isDead", true);//�������
            scoreCounter.CountScore(300);
            GetComponent<Collider>().transform.gameObject.layer = LayerMask.NameToLayer("dead");
            canvas.SetActive(false);
        }
        dead = true;
        //Destroy(this.gameObject);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainPlayer");
        scoreCounter = FindObjectOfType<ScoreCounter>();
        StartCoroutine(CheckPLayer());
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !strikesNow && hp > 0 && !dead)//�������
        {
            strikesNow = true;
            Strike();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        strikesNow = false;
    }

    private IEnumerator StrikeTimer()
    {
        yield return new WaitForSeconds(attackSpeed);
        while (strikesNow && !dead)
        {
            if (animator.GetBool("isHited"))
            {
                strikesNow = false;
                break;
            }
            if (Math.Abs(rb.velocity.x) < 1f)
            {
                // ������� �� �������
                foreach (var item in Physics.OverlapBox(gun.transform.position,
                        new Vector3(1, 1, 1),
                        Quaternion.identity, 8))
                {
                    if (player.transform.position.x - gun.transform.position.x < xRange)
                    {
                        animator.SetBool("isPunching", true);
                        yield return new WaitForSeconds(0.5f);
                        if (!animator.GetBool("isHited"))
                        {
                            item.GetComponentInParent<LiveCycle>()?.GetDamage(enemyDamage);
                            onPunch?.Invoke();
                        }
                        else
                        {
                            yield return new WaitForSeconds(0.5f);
                            animator.SetBool("isHited", false);
                        }
                    }
                    break;
                }
                yield return new WaitForSeconds(attackSpeed);
            }
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }

    private void Update()
    {
        animator.SetFloat("hSpeed", Math.Abs(GetComponent<Rigidbody>().velocity.x));
        RunTo();
    }

    private IEnumerator CheckPLayer()
    {
        while (true)
        {

            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

            if (rangeChecks.Length != 0)
            {
                Transform target = rangeChecks[0].transform;

                Vector3 directionToTarget = (target.position - transform.position).normalized;
                RaycastHit hit;
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                //Debug.Log($"{Physics.Raycast(transform.position, directionToTarget, out hit, distanceToTarget * 2f, obstructionMask)} + {hit.collider}");
                if (Physics.Raycast(transform.position, directionToTarget, distanceToTarget * 2f, obstructionMask) && 
                                    Mathf.Abs(transform.position.y - target.position.y) < .5f)
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
            yield return new WaitForSeconds(0.3f);
        }
    }
}
