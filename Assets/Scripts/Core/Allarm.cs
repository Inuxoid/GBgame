using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Allarm : MonoBehaviour
{
    public bool IsAlarming;

    [SerializeField] private UnAlarmButton unAlarmButton;

    public void StartAlarm()
    {
        IsAlarming = true;
        unAlarmButton.IsOpened = true;
    }

    public void EndAlarm()
    {
        IsAlarming = false;
        unAlarmButton.IsOpened = false;
    }
}
