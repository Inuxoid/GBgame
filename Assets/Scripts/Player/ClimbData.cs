using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClimbData : MonoBehaviour
{
    [SerializeField] private Vector3 point1;
    [SerializeField] private Vector3 point2;
    [SerializeField] private float x1;
    [SerializeField] private float y1;
    [SerializeField] private float x2;
    [SerializeField] private float y2;
    [SerializeField] private float z;
    [SerializeField] private float mult;

    public Vector3 Point1 { get => point1; set => point1 = value; }
    public Vector3 Point2 { get => point2; set => point2 = value; }

    private void Start()
    {
        point1 = new Vector3(transform.position.x + x1 * mult, transform.position.y + y1, z);
        point2 = new Vector3(transform.position.x + x2 * mult, transform.position.y + y2, z);
    }
}
