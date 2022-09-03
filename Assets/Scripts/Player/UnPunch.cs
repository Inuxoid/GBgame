using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnPunch : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void EndPunch()
    {
        Debug.Log("unpinched");
        animator.SetBool("isPunching", false);
        animator.SetBool("secondPunch", !animator.GetBool("secondPunch"));
    }
}
