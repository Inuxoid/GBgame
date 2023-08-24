using Dto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Turret : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject turRotate;
    [SerializeField] private GameObject mainObj;
    [SerializeField] private Material lens;
    [SerializeField] private ScoreCounter scoreCounter;

    [Header("Settings")]
    [SerializeField] private bool canRotate;
    [SerializeField] private float checkTimer;
    [SerializeField] private float maxHP;
    [SerializeField] private float hp;
    [SerializeField] private int enemyDamage;
    [SerializeField] private bool canSeePlayer;
    [SerializeField] private bool isStrikes;
    [SerializeField] private bool isRotating;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstructionMask;
    [SerializeField] private float radius;
    [SerializeField] private float attackSpeed;
    [SerializeField] private int flip = 1;
    [SerializeField] private UnityEvent<FloatNumberDto> onHpChanged;
    [SerializeField] private UnityEvent onShooted;
    [SerializeField] private UnityEvent onDestroyed;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float rotateSpeed;

    public bool CanSeePlayer
    {
        get => canSeePlayer;
        set
        {
            canSeePlayer = value;
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
        flip *= -1;
        turRotate.transform.Rotate(0, 180, 0);
    }

    public void Strike()
    {
        StartCoroutine(StrikeTimer());  
    }

    public void MoveToPlayer()
    {
        if ((flip * (player.transform.position.x - this.transform.position.x) < 0) && canRotate)
        {
            Flip();
        }
    }

    public void Death()
    {
        scoreCounter.CountScore(300);
        mainObj.transform.Rotate(new Vector3(0, 0, -40));
        lens.SetColor("_EmissionColor", new Color(0, 0, 0, 1.0F));
        onDestroyed?.Invoke();
        Destroy(this);
    }

    private void Start()
    {
        scoreCounter = FindObjectOfType<ScoreCounter>();
        player = GameObject.FindGameObjectWithTag("MainPlayer");
        StartCoroutine(CheckPlayer());
    }

    private void Update()
    {
        if (CanSeePlayer && !isRotating)
        {
            StartCoroutine(RotateTimer());
        }

        if (canSeePlayer && !isStrikes)
        {
            Strike();
            //Debug.Log("Strike");
        }
    }


    private IEnumerator StrikeTimer()
    {
        isStrikes = true;
        while (canSeePlayer && flip * (player.transform.position.x - this.transform.position.x) > 0)
        {
            GameObject go = Instantiate(bullet, transform.position, Quaternion.identity);
            go.GetComponent<Rigidbody>().AddForce(new Vector3((player.transform.position.x - transform.position.x),
                                                              0, 0).normalized * bulletSpeed, ForceMode.Impulse);
            onShooted?.Invoke();
            yield return new WaitForSeconds(attackSpeed);
        }
        isStrikes = false;
        yield return null;
    }

    private IEnumerator RotateTimer()
    {
        isRotating = true;
        yield return new WaitForSeconds(rotateSpeed);
        MoveToPlayer();
        isRotating = false;
        yield return null;
    }

    private IEnumerator CheckPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkTimer);
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

            if (rangeChecks.Length != 0)
            {
                Transform target = rangeChecks[0].transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;

                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (Physics.Raycast(transform.position, directionToTarget, distanceToTarget * 2, obstructionMask) &&
                                    Mathf.Abs(transform.position.y - target.position.y) < radius)
                {
                    CanSeePlayer = true;
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
}
