using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VHS : MonoBehaviour
{
    [SerializeField] private UnityEvent onCoinTaken;
    [SerializeField] private ScoreCounter scoreCounter;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            scoreCounter.CountVHS();
            scoreCounter.CountScore(100);
            this.onCoinTaken?.Invoke();
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        scoreCounter = FindObjectOfType<ScoreCounter>();
    }
}
