using Dto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreCounter : MonoBehaviour
{
    [Header("Output")]
    [SerializeField] private int scoreCount;
    [SerializeField] private UnityEvent<IntNumberDto> onCounted;
    [SerializeField] private UnityEvent onAllScoreTaken;

    public int ScoreCount { get => scoreCount; set => scoreCount = value; }


    public void Count()
    {
        ScoreCount++;
        IntNumberDto dto = new IntNumberDto() { value = this.ScoreCount };
        this.onCounted?.Invoke(dto);
    }
}
