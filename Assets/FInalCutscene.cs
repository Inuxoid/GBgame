using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FInalCutscene : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("Play", true);
        }
        
    }
}
