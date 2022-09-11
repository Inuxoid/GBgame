using Dto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cam : MonoBehaviour
{
    [SerializeField] private Allarm allarm;
    [SerializeField] private float maxTimer;
    [SerializeField] private float timer;
    [SerializeField] private bool inZone;
    [SerializeField] private bool done;
    [SerializeField] private float pause;
    [SerializeField] private UnityEvent<FloatNumberDto> onTimerChanged;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public float Timer { get => timer; set => timer = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inZone = false;
        }
    }

    private void Update()
    {
        if (Timer >= 0 && inZone && !done && !allarm.IsAlarming)
        {
            Timer -= Time.deltaTime;
            FloatNumberDto dto = new FloatNumberDto { value = Timer / maxTimer };
            onTimerChanged?.Invoke(dto);
        }

        if (Timer <= maxTimer && !inZone && !allarm.IsAlarming)
        {
            Timer += Time.deltaTime;
            FloatNumberDto dto = new FloatNumberDto { value = Timer / maxTimer };
            onTimerChanged?.Invoke(dto);
        }

        if (Timer <= 0 && !done)
        {
            done = true;
            Timer = maxTimer;
            allarm.StartAlarm();
        }

        if (Timer >= maxTimer && done && !allarm.IsAlarming)
        {
            StartCoroutine(PauseCamTimer());
        }

        if (allarm.IsAlarming)
        {
            
        }
    }

    IEnumerator PauseCamTimer()
    {
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(pause);
        spriteRenderer.enabled = true;
        done = false;
        yield return null;
    }
}
