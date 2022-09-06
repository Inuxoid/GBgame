using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    [SerializeField] private bool isSpawning;

    public bool IsSpawning { get => isSpawning; set => isSpawning = value; }

    private void Awake()
    {
        StartCoroutine(SpawningTimer());
    }

    public void Spawn()
    {
        Instantiate(gameObject);
    }

    IEnumerator SpawningTimer()
    {
        while (IsSpawning)
        {
            Spawn();
            yield return new WaitForSeconds(3f);
        }
        yield return null;
    }
}
