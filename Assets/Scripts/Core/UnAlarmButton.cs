using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnAlarmButton : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject[] door;
    [SerializeField] private GameObject camColor;
    [SerializeField] private Cam[] cams;
    [Header("Settings")]
    [SerializeField] private bool isOpened;
    [SerializeField] private bool isTrying;
    [SerializeField] private bool isOpening;
    [SerializeField] private float y;
    [SerializeField] private float x;
    [SerializeField] private UnityEvent onPressed;

    [SerializeField] private Allarm allarm;

    public bool IsOpened
    {
        get => isOpened; set
        {
            isOpened = value;
            DoorPositionChange();
        }
    }

    public void DoorPositionChange()
    {
        foreach (var item in door)
        {
            if (isOpened)
            {
                item.SetActive(true);
            }
            else
            {
                item.SetActive(false);
            }
        }
    }

    public void StopAlarm()
    {
        allarm.EndAlarm();
        foreach (var item in cams)
        {
            item.PauseCam();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isTrying = true;

        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            isTrying = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && isTrying && !isOpening && allarm.IsAlarming)
        {
            onPressed?.Invoke();
            StartCoroutine(ButtonTimer());
        }
    }

    IEnumerator ButtonTimer()
    {
        isOpening = true;
        IsOpened = !IsOpened;
        StopAlarm();
        yield return new WaitForSeconds(1f);
        allarm.IsAlarming = false;
           
        isOpening = false;
        yield return null;
    }
}
