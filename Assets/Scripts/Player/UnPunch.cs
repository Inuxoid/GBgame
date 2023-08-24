using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnPunch : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void EndPunch()
    {
        animator.SetBool("isPunching", false);
        animator.SetBool("isStrongPunching", false);
        animator.SetBool("isStrongPunchingFinish", false);
        animator.SetBool("isPunchingWeak", false);
        animator.SetBool("isShooting", false);
        animator.SetBool("secondPunch", !animator.GetBool("secondPunch"));
    }
}
