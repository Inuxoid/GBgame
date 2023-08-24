using System.Collections.Generic;
using Meta.Upgrades.Controller;
using StateMachines.PlayerSM;
using StateMachines.PlayerSM.States;

namespace Meta.Upgrades.UpgradeSkills
{
    public class Nanomachines : Upgrade
    {
        private readonly List<int> grades = new()
        {
            125, 
            135, 
            150
        };
        
        public int curGrade;
        
        private void LoadGrades()
        {
            curGrade = grades[Level - 1];
        }
        
        public override void Activate(PlayerSM playerSm)
        {
            LoadGrades();
            playerSm.liveCycle.SetMaxHP(curGrade);
        }

        public override void Deactivate(PlayerSM playerSm)
        {
            playerSm.liveCycle.SetMaxHP(100); // Base max hp
        }
    }
}