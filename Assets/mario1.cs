using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class mario1 : MonoBehaviour
{
    [SerializeField] private UnityEvent onDestroy;
    public GameObject block;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onDestroy?.Invoke();
            Destroy(this.gameObject);
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
