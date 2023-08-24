using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Heart : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int hpRes;
    [SerializeField] private UnityEvent OnTake;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponentInParent<LiveCycle>().Hp != 100)
        {
            OnTake?.Invoke();
            other.GetComponentInParent<LiveCycle>().Heal(hpRes);
            Destroy(GetComponent<Collider>());
            StartCoroutine(DestroyTimer());
        }
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        yield return null;
    }
}
