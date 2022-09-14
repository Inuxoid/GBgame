using Dto;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Events;

public class Cam : MonoBehaviour
{
    [SerializeField] private Allarm allarm;
    [SerializeField] private Cam[] others;
    [SerializeField] private float maxTimer;
    [SerializeField] private float timer;
    [SerializeField] private bool inZone;
    [SerializeField] private bool paused;
    [SerializeField] private float pause;
    [SerializeField] private UnityEvent<FloatNumberDto> onTimerChanged;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public float Timer { get => timer; set => timer = value; }
    public bool Paused { get => paused; set => paused = value; }

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
        if (Timer >= 0 && inZone && !allarm.IsAlarming && !CheckPause())
        {
            Timer -= Time.deltaTime;
            FloatNumberDto dto = new FloatNumberDto { value = Timer / maxTimer };
            onTimerChanged?.Invoke(dto);
        }

        if (Timer <= maxTimer && !inZone && !allarm.IsAlarming)
        {
            Timer += Time.deltaTime * 2;
            FloatNumberDto dto = new FloatNumberDto { value = Timer / maxTimer };
            onTimerChanged?.Invoke(dto);
        }

        if (Timer <= 0 && !allarm.IsAlarming && !CheckPause())
        {
            allarm.StartAlarm();
        }
    }

    private bool CheckPause()
    {
        foreach (var item in others)
        {
            if (item.Paused == true)
            {
                return true;
            }
        }
        return false;
    }

    public void PauseCam()
    {
        if (!Paused)
            StartCoroutine(PauseCamTimer());
    }

    IEnumerator PauseCamTimer()
    {
        Paused = true;
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(pause);
        spriteRenderer.enabled = true;
        Paused = false;
        yield return null;
    }
}
