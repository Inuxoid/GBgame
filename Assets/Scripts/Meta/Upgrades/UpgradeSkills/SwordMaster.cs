using System.Collections.Generic;
using Meta.Upgrades.Controller;
using StateMachines.PlayerSM;
using UnityEngine;

namespace Meta.Upgrades.UpgradeSkills
{
    public class SwordMaster : Upgrade
    {
        private readonly List<(bool, bool, bool)> grades = new()
        {
            (true, false, false),
            (true, true, false),
            (true, true, true)
        };
        
        public (bool, bool, bool) curGrade;
        
        private void LoadGrades()
        {
            curGrade = grades[Level - 1];
        }
        
        public override void Activate(PlayerSM playerSm)
        {
            base.Activate(playerSm);
            LoadGrades();
            playerSm.GetComponentInChildren<Animator>().SetBool("FirstCombo", curGrade.Item1);
            playerSm.GetComponentInChildren<Animator>().SetBool("SecondCombo", curGrade.Item2);
            playerSm.GetComponentInChildren<Animator>().SetBool("FinishCombo", curGrade.Item3);
            Debug.Log($"Activated {curGrade.Item1} {curGrade.Item2} {curGrade.Item3}");
            Debug.Log($"Activated {playerSm.GetComponentInChildren<Animator>().GetBool("FirstCombo")} {playerSm.GetComponentInChildren<Animator>().GetBool("SecondCombo")} {playerSm.GetComponentInChildren<Animator>().GetBool("FinishCombo")}");
        }

        public override void Deactivate(PlayerSM playerSm)
        {
            Debug.Log("DeActivated");
            base.Deactivate(playerSm);
            LoadGrades();
            playerSm.GetComponentInChildren<Animator>().SetBool("FirstCombo", false);
            playerSm.GetComponentInChildren<Animator>().SetBool("SecondCombo", false);
            playerSm.GetComponentInChildren<Animator>().SetBool("FinishCombo", false);
        }
    }
}