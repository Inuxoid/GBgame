using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Dto;

public class ClickCounter : MonoBehaviour
{
    [Header("Output")]
    [SerializeField] private int clicksCount;
    public UnityEvent<IntNumberDto> onCounted;
    public UnityEvent onVictory;
    public void Count()
    {
        clicksCount++;
        IntNumberDto dto = new IntNumberDto() { value = this.clicksCount };
        this.onCounted?.Invoke(dto);
        if (clicksCount == 10)
        {
            this.onVictory?.Invoke();
        }
    }
}
