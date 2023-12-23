using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnClimb : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void EndClimb()
    {
        //animator.SetBool("isClimbing", false);
    }
}
