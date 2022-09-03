using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 point1;
    [SerializeField] private Vector3 point2;
    [SerializeField] private float passDistance;
    [SerializeField] private float speed;
    [SerializeField] private Vector3 target;
    [SerializeField] private Transform colTransform;
    [SerializeField] private Transform playerModel;

    public Vector3 Point1 { get => point1; set => point1 = value; }
    public Vector3 Point2 { get => point2; set => point2 = value; }

    public void FirstMove()
    {
        target = point1;
    }

    public void SecondMove()
    {
        target = point2;
    }

    private void Update()
    {
        if (target != Vector3.zero)
        {
            //player.transform.position = target.position;
            player.transform.position = Vector3.MoveTowards(player.transform.position, new Vector3(target.x, target.y, target.z), Time.deltaTime * speed);


            //colTransform.position = Vector3.MoveTowards(player.transform.position, target.position, Time.deltaTime * speed);
        }
        if (Vector3.Distance(player.transform.position, Point1) < passDistance)
        {
            Debug.Log("FirstDone");
            playerModel.localPosition = new Vector3(0, -0.949f, 0);
            SecondMove();
        }

        if (Vector3.Distance(player.transform.position, Point2) < passDistance)
        {
            Debug.Log("SecondDone");
            playerModel.localPosition = new Vector3(0, -0.949f, 0);
            animator.SetBool("isClimbing", false);
            target = Vector3.zero;
        }
    }
}
