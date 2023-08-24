using System;
using UnityEngine;

public class ClimbData : MonoBehaviour
{
  [SerializeField] private Transform[] points;
  [SerializeField] private AnimationCurve climbCurve;
    [SerializeField] private bool twoSides;

  private Vector3[] pointsPosition;
  private Vector3[] reversePointsPosition;

  private void Awake()
  {
    pointsPosition = new Vector3[points.Length];
    reversePointsPosition = new Vector3[points.Length];
    
    for (int i = 0; i < points.Length; i++)
    {
      pointsPosition[i] = points[i].position;
    }
    
    for (int i = points.Length-1; i >= 0; i--)
    {
      reversePointsPosition[points.Length-1-i] = points[i].position;
    }
  }

  public Vector3 FirstPoint(bool isPlayerLeft)
  {
    if (isPlayerLeft || !twoSides)
      return pointsPosition[0];
        //Debug.Log($"{reversePointsPosition[0]}, {reversePointsPosition[1]}, {reversePointsPosition[2]}, {reversePointsPosition[3]}");
    return reversePointsPosition[0];
  }

  public Vector3[] Points(bool isPlayerLeft)    
  {
    if (isPlayerLeft || !twoSides)
      return pointsPosition;
    return reversePointsPosition;
  }
}