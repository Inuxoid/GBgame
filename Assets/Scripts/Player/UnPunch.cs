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
        
        if (animator.GetBool("secondPunch") && animator.GetBool("FirstCombo"))
        {
            animator.SetInteger("punchStreak", 1);
            animator.SetBool("secondPunch", false);
        }
        else
        {
            animator.SetBool("secondPunch", !animator.GetBool("secondPunch"));
            animator.SetInteger("punchStreak", 0);
        }

        if (animator.GetBool("SecondCombo") && animator.GetInteger("punchStreak") == 1)
        {
            animator.SetInteger("punchStreak", 2);
        }
        
        if (animator.GetBool("FinishCombo") && animator.GetInteger("punchStreak") == 2)
        {
            animator.SetInteger("punchStreak", 3);
        }
        
    }
}
