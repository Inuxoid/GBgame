using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnHit : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void EndHit()
    {
        animator.SetBool("isHited", false);
        Debug.Log("animator.SetBool(\"isHited\", false))");
    }
}