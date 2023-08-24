using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    [SerializeField] private bool isSpawning;
    [SerializeField] private GameObject spawningGameObject;
    public bool IsSpawning { get => isSpawning; set => isSpawning = value; }

    private void Awake()
    {
        StartCoroutine(SpawningTimer());
    }

    public void Spawn()
    {
        Instantiate(spawningGameObject, transform);
    }

    IEnumerator SpawningTimer()
    {
        while (true)
        {
            if (isSpawning)
                Spawn();
            yield return new WaitForSeconds(10f);
        }
    }
}
