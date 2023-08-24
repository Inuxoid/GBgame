using Dto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreCounter : MonoBehaviour
{
    [Header("Output")]
    [SerializeField] private int vhsCount;
    [SerializeField] private int scoreCount;
    [SerializeField] private UnityEvent<IntNumberDto> onVHSCounted;
    [SerializeField] private UnityEvent<IntNumberDto> onScoreCounted;

    public int VHSCount { get => vhsCount; set => vhsCount = value; }
    public int ScoreCount { get => scoreCount; set => scoreCount = value; }

    public void CountScore(int score)
    {
        ScoreCount += score;
        IntNumberDto dto = new IntNumberDto() { value = this.ScoreCount };
        this.onScoreCounted?.Invoke(dto);
    }

    public void CountVHS()
    {
        VHSCount++;
        IntNumberDto dto = new IntNumberDto() { value = this.VHSCount };
        this.onVHSCounted?.Invoke(dto);
    }

    public void LoadVHS(int VHS)
    {
        VHSCount += VHS;
        IntNumberDto dto = new IntNumberDto() { value = this.VHSCount };
        this.onVHSCounted?.Invoke(dto);
    }
}
    