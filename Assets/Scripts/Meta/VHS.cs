using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VHS : MonoBehaviour
{
    [SerializeField] private ScoreCounter scoreCounter;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            scoreCounter.CountScore(100);
            scoreCounter.CountVHS();
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        scoreCounter = FindObjectOfType<ScoreCounter>();
    }
}
