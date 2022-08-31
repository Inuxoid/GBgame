using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResult : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool alreadyWon = false;
    public void Victory()
    {
        Debug.Log("Victory!");
        alreadyWon = true;
    }

    public void Defeat()
    {
        if (alreadyWon) return;
        Debug.Log("Defeat!");
    }
}
