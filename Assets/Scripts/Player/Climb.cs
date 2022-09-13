using System;
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
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private bool second;

    public Vector3 Point1 { get => point1; set => point1 = value; }
    public Vector3 Point2 { get => point2; set => point2 = value; }

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    public void FirstMove()
    {
        rb.velocity = Vector3.zero;
        target = point1;
    }

    public void SecondMove()
    {
        target = point2;
        second = true;
        // Проверить-
    }

    private void Update()
    {
        if (target != Vector3.zero && (Vector3.Distance(player.transform.position, target) < 1.5f || second))
        {
            // Точки
            // В корутину
            // Другие климбы
            //player.transform.position = target.position;
            Debug.Log("First");
            Debug.Log(Vector3.Distance(player.transform.position, target));
            playerMovement.SpeedMod = 0;
            player.transform.position = Vector3.MoveTowards(player.transform.position, new Vector3(target.x, target.y, target.z), Time.deltaTime * speed);
            //colTransform.position = Vector3.MoveTowards(player.transform.position, target.position, Time.deltaTime * speed);
        }
        else
        {
            target = Vector3.zero;
            animator.SetBool("isClimbing", false);
            playerModel.localPosition = new Vector3(0, -0.949f, 0);
        }

        if (target != Vector3.zero && Vector3.Distance(player.transform.position, Point1) < passDistance && !second)
        {
            Debug.Log("Second");
            playerModel.localPosition = new Vector3(0, -0.949f, 0);
            SecondMove();
        }
        else if (target != Vector3.zero && Vector3.Distance(player.transform.position, Point2) < passDistance && second)
        {
            Debug.Log("Third");
            animator.SetBool("isClimbing", false);
            playerModel.localPosition = new Vector3(0, -0.949f, 0);
            target = Vector3.zero;
            playerMovement.SpeedMod = 1;
            second = false;
        }
    }
}
