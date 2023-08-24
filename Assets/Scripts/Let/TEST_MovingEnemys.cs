using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_MovingEnemys : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform[] points;
    [Header("Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float passDistance;
    [SerializeField] private int next;

    public Transform[] Points { get => points; set => points = value; }

    void Update()
    {
        Move();
    }

    public void Move()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, Points[next].position, Time.deltaTime * speed);
        if (Vector3.Distance(this.transform.position, Points[next].position) < passDistance)
        {
            if (next == Points.Length - 1)
            {
                next = 0;
                this.transform.Rotate(0f, -180f, 0f, Space.Self);
            }
            else
            {
                next++;
                this.transform.Rotate(0f, 180f, 0f, Space.Self);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponentInParent<PlayerController>())
        {
            collision.collider.gameObject.transform.parent.parent = this.transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.GetComponentInParent<PlayerController>())
        {
            collision.collider.gameObject.transform.parent.parent = null;
        }
    }
}
