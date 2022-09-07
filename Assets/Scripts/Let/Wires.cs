using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wires : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int damage;
    [SerializeField] private UnityEvent<int> onDamaged;
    [SerializeField] private GameObject effect;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(lightiningTimer());
            this.onDamaged?.Invoke(damage);
        }
    }

    IEnumerator lightiningTimer()
    {
        effect.SetActive(true);
        yield return new WaitForSeconds(1f);
        effect.SetActive(false);
        yield return null;
    }
}
