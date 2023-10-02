using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnPunch : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float resetTime = 3.0f;
    private Coroutine punchTimerCoroutine;

    private void Update()
    {
        if (animator.GetBool("isPunching"))
        {
            if (punchTimerCoroutine != null)
            {
                StopCoroutine(punchTimerCoroutine);
                punchTimerCoroutine = null;
            }
        }
        else
        {
            if (punchTimerCoroutine == null)
            {
                punchTimerCoroutine = StartCoroutine(PunchTimer());
            }
        }
    }

    private IEnumerator PunchTimer()
    {
        yield return new WaitForSeconds(resetTime);
        ResetPunch();
    }
    
    public void EndPunch()
    {
        ResetAnimatorBools();
        
        int punchStreakValue = animator.GetInteger("punchStreak");
        
        if (punchStreakValue == 3 || animator.GetBool("isPunchingWeak"))
        {
            ResetPunch();
        }
        else if (punchStreakValue == 2)
        {
            HandleSecondPunchCombo();
        }
        else if (punchStreakValue == 1)
        {
            HandleFirstPunchCombo();
        }
        else
        {
            HandleInitialPunch();
        }
    }

    private void ResetAnimatorBools()
    {
        animator.SetBool("isPunching", false);
        animator.SetBool("isStrongPunching", false);
        animator.SetBool("isStrongPunchingFinish", false);
        animator.SetBool("isPunchingWeak", false);
        animator.SetBool("isShooting", false);
    }
    
    private void ResetPunch()
    {
        animator.SetInteger("punchStreak", 0);
        animator.SetBool("secondPunch", false);
    }

    private void HandleSecondPunchCombo()
    {
        if (animator.GetBool("FinishCombo"))
        {
            animator.SetInteger("punchStreak", 3);
        }
        else
        {
            animator.SetInteger("punchStreak", 0);
            animator.SetBool("secondPunch", false);
        }
    }

    private void HandleFirstPunchCombo()
    {
        if (animator.GetBool("SecondCombo"))
        {
            animator.SetInteger("punchStreak", 2);
        }
        else
        {
            animator.SetInteger("punchStreak", 0);
            animator.SetBool("secondPunch", false);
        }
    }

    private void HandleInitialPunch()
    {
        if (animator.GetBool("secondPunch") && animator.GetBool("FirstCombo"))
        {
            animator.SetInteger("punchStreak", 1);
            animator.SetBool("secondPunch", false);
        }
        else
        {
            animator.SetBool("secondPunch", !animator.GetBool("secondPunch"));
        }
    }
}
