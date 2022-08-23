using Dto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Turret : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody bullet;
    [SerializeField] private GameObject spawnPoint;

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
    [SerializeField] private bool strikesNow;
    [SerializeField] private UnityEvent<FloatNumberDto> onHpChanged;

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
        StartCoroutine(ShootTimer());
    }

    public void MoveToPlayer()
    {
        if (!(player.transform.position.x - this.transform.position.x > 0))
        {
            Flip();
        }
    }

    public void Controller()
    {
        if (CanSeePlayer)
        {
            MoveToPlayer();
            StartCoroutine(ShootTimer());
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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !strikesNow)
        {
            Strike();
            strikesNow = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        strikesNow = false;
    }

    private IEnumerator ShootTimer()
    {
        while (true)
        {
            foreach (var item in Physics.OverlapBox(new Vector3(this.transform.position.x + flip, this.transform.position.y),
                                    new Vector3(1, 1, 1),
                                    Quaternion.identity, 8))
            {
                Rigidbody bulletInstance;
                bulletInstance = Instantiate(bullet, spawnPoint.transform.position, spawnPoint.transform.rotation);
                bulletInstance.AddForce(item.transform.position - spawnPoint.transform.position * 10f);
            }
            yield return new WaitForSeconds(attackSpeed);
        }
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
                                    Mathf.Abs(transform.position.y - target.position.y) < 0.7f)
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
