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
    [SerializeField] private GameObject tur;

    [Header("Settings")]
    [SerializeField] private float maxHP;
    [SerializeField] private float hp;
    [SerializeField] private int enemyDamage;
    [SerializeField] private bool canSeePlayer;
    [SerializeField] private bool isStrikes;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstructionMask;
    [SerializeField] private float radius;
    [SerializeField] private float attackSpeed;
    [SerializeField] private int flip = 1;
    [SerializeField] private UnityEvent<FloatNumberDto> onHpChanged;
    [SerializeField] private float bulletSpeed;

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
        Debug.Log("asf");
        flip *= -1;
        tur.transform.Rotate(0, 180, 0);
    }

    public void Strike()
    {
        StartCoroutine(StrikeTimer());
    }

    public void MoveToPlayer()
    {
        Debug.Log(flip * (player.transform.position.x - this.transform.position.x));
        if (flip * (player.transform.position.x - this.transform.position.x) < 0)
        {
            Flip();
        }
    }

    public void Death()
    {
        Destroy(this.gameObject);
    }

    private void Start()
    {
        StartCoroutine(CheckPLayer());
    }

    private void Update()
    {
        if (CanSeePlayer)
        {
            MoveToPlayer();
        }

        if (canSeePlayer && !isStrikes)
        {
            Strike();
        }
    }


    private IEnumerator StrikeTimer()
    {
        isStrikes = true;
        while (canSeePlayer)
        {
            GameObject go = Instantiate(bullet, transform.position, Quaternion.identity);
            go.GetComponent<Rigidbody>().AddForce(new Vector3((player.transform.position.x - transform.position.x),
                                                              0, 0).normalized * bulletSpeed, ForceMode.Impulse);
            yield return new WaitForSeconds(attackSpeed);   
        }
        isStrikes = false;
        yield return null;
    }

    private IEnumerator CheckPLayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

            if (rangeChecks.Length != 0)
            {
                Transform target = rangeChecks[0].transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;

                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask) &&
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
