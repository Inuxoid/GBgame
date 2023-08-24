using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_RotationEnemys1 : MonoBehaviour
{
    [SerializeField] private float time;

    private float _timeLeft;

    private IEnumerator StartTimer()
    {
        while (_timeLeft > 0)
        {
            
            _timeLeft -= Time.deltaTime;
            yield return null;
        }

    }

    private void Start()
    {
        _timeLeft = time;
        StartCoroutine(StartTimer());
    }
    private void Update()
    {
        if (_timeLeft <= 0)
        {
            this.transform.Rotate(0f, 180f, 0f, Space.Self);
            _timeLeft = time;
            
        }
        
    }

}
