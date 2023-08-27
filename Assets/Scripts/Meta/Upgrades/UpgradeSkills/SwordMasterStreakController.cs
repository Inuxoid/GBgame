using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.Upgrades.UpgradeSkills
{
    public class SwordMasterStreakController : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        public void UnStreak()
        {
            animator.SetInteger("punchStreak", 0);
            animator.SetBool("secondPunch", false);
        }
    }
}