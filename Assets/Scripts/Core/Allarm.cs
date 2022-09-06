using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Allarm : MonoBehaviour
{
    public bool IsAlarming;

    [SerializeField] private UnAlarmButton unAlarmButton;
    [SerializeField] private Spawner spawner;

    public void StartAlarm()
    {
        IsAlarming = true;
        unAlarmButton.IsOpened = true;
        spawner.IsSpawning = true;
    }

    public void EndAlarm()
    {
        IsAlarming = false;
        unAlarmButton.IsOpened = false;
        spawner.IsSpawning = false;
        GameObject[] cyborgs = GameObject.FindGameObjectsWithTag("Cyborg");
        foreach (GameObject go in cyborgs)
        {
            Destroy(go);
        }
    }
}
