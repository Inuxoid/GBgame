using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClimbData : MonoBehaviour
{
    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;

    public Transform Point1 { get => point1; set => point1 = value; }
    public Transform Point2 { get => point2; set => point2 = value; }
}
