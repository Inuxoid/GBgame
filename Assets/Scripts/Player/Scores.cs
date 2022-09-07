using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scores : MonoBehaviour
{
    [SerializeField] private int score;

    public int Score { get => score; set => score = value; }

    public void AddScore(int count)
    {
        Score += count;
    }
}
