using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlarform : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform[] points;
    [Header("Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float passDistance;
    [SerializeField] private int next;

    void Update()
    {
        Move();
    }

    public void Move()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, points[next].position, Time.deltaTime * speed);
        if (Vector3.Distance(this.transform.position, points[next].position) < passDistance)
        {
            if (next == points.Length - 1)
            {
                next = 0;
            }
            else
            {
                next++;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.transform.parent.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.transform.parent.parent = null;
        }
    }
}
