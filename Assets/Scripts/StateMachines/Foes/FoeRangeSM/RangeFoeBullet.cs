using UnityEngine;

public class RangeFoeBullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;

    private Vector3 direction;

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    private void Update()
    {
        transform.position += direction * (speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent.GetComponent<LiveCycle>().GetDamage(damage);
            Destroy(transform.parent.gameObject);
        }
        Destroy(transform.parent.gameObject);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(transform.parent.gameObject);
    }
}