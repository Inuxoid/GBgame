using System.Collections.Generic;
using Meta.Upgrades.Controller;
using StateMachines.PlayerSM;
using UnityEngine;

namespace Meta.Upgrades.UpgradeSkills
{
    public class HyperphasicKatana : Upgrade
    {
        private readonly List<(float, float, float)> grades = new()
        {
            (1.2f, 0f, 0f),
            (1.2f, 0.15f, 1.5f),
            (1.2f, 0.3f, 2f)
        };

        public (float, float, float) curGrade;
        
        private void LoadGrades()
        {
            curGrade = grades[Level - 1];
        }

        public override void Activate(PlayerSM playerSm)
        {
            base.Activate(playerSm);
            LoadGrades();
            playerSm.attackRange *= curGrade.Item1;
            playerSm.critRate = curGrade.Item2;
            playerSm.critValue = curGrade.Item3;
            playerSm.UpdateKatanaModel(); 
        }

        public override void Deactivate(PlayerSM playerSm)
        {
            base.Deactivate(playerSm);
            LoadGrades();
            playerSm.attackRange /= curGrade.Item1;
            playerSm.critRate = curGrade.Item2;
            playerSm.critValue = curGrade.Item3;
            playerSm.UpdateKatanaModel(); 
        }
    }
}