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
        
        if (animator.GetInteger("punchStreak") == 3)
        {
            animator.SetInteger("punchStreak", 0);
            animator.SetBool("secondPunch", false);
            return;
        }
        
        if (animator.GetBool("FinishCombo") && animator.GetInteger("punchStreak") == 2)
        {
            animator.SetInteger("punchStreak", 3);    
            return;
        }
        else if (!animator.GetBool("FinishCombo"))
        {
            animator.SetInteger("punchStreak", 0);
            animator.SetBool("secondPunch", false);
            return;
        }

        if (animator.GetBool("SecondCombo") && animator.GetInteger("punchStreak") == 1)
        {
            animator.SetInteger("punchStreak", 2);
            return;
        }
        else if (!animator.GetBool("SecondCombo"))
        {
            animator.SetInteger("punchStreak", 0);
            animator.SetBool("secondPunch", false);
            return;
        }
                                            
        if (animator.GetBool("secondPunch") && animator.GetBool("FirstCombo") && animator.GetInteger("punchStreak") == 0)
        {
            animator.SetInteger("punchStreak", 1);
            animator.SetBool("secondPunch", false);
            return;
        }
        else
        {
            animator.SetBool("secondPunch", true);
            return;
        }    
    }
}
