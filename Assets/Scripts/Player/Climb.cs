using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform player;
    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;
    [SerializeField] private float passDistance;
    [SerializeField] private float speed;

    public Transform Point1 { get => point1; set => point1 = value; }
    public Transform Point2 { get => point2; set => point2 = value; }

    public void FirstMove()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, Point1.position, Time.deltaTime * speed);
        if (Vector3.Distance(this.transform.position, Point1.position) < passDistance)
        {
            SecondMove();
        }
    }

    public void SecondMove()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, Point2.position, Time.deltaTime * speed);
        if (Vector3.Distance(this.transform.position, Point2.position) < passDistance)
        {
            animator.SetBool("isClimbing", false);
        }
    }
}
