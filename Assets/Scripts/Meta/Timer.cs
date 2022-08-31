using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Dto;

public class Timer : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField][Range(3, 7)] private float timeToLose;

    public UnityEvent<FloatNumberDto> onTimeChanged;
    public UnityEvent onTimeEnd;


    private void Start()
    {
        FloatNumberDto dto = new FloatNumberDto() { value = this.timeToLose };
        this.onTimeChanged?.Invoke(dto); 

        StartCoroutine(SecondsTimer());
    }

    private IEnumerator SecondsTimer()
    {
        while (this.timeToLose != 0)
        {
            yield return new WaitForSeconds(1);
            this.timeToLose--;
            FloatNumberDto dto = new FloatNumberDto() { value = this.timeToLose };
            this.onTimeChanged?.Invoke(dto);
        }

        this.onTimeEnd?.Invoke();
    }
}
