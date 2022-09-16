using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Allarm : MonoBehaviour
{
    private bool isAlarming;

    [SerializeField] private UnAlarmButton unAlarmButton;
    [SerializeField] private Spawner[] spawners;
    [SerializeField] private GameObject hz;
    [SerializeField] private UnityEvent onAllarm;
    [SerializeField] private UnityEvent onUnAllarm;

    public bool IsAlarming { get => isAlarming; set => isAlarming = value; }

    public void StartAlarm()
    {
        onAllarm?.Invoke();
        IsAlarming = true;
        hz.SetActive(true);
        unAlarmButton.IsOpened = true;
        foreach (Spawner spawner in spawners)
        {
            spawner.IsSpawning = true;
        }
    }

    public void EndAlarm()
    {
        onUnAllarm?.Invoke();
        IsAlarming = false;
        hz.SetActive(false);
        unAlarmButton.IsOpened = false;
        foreach (Spawner spawner in spawners)
        {
            spawner.IsSpawning = false;
        }
        GameObject[] cyborgs = GameObject.FindGameObjectsWithTag("Cyborg");
        foreach (GameObject go in cyborgs)
        {
            Destroy(go);
        }
    }
}
