using System;
using System.Collections.Generic;
using Meta.Upgrades.Controller;
using Meta.Upgrades.View.HUD;
using StateMachines.PlayerSM;
using StateMachines.PlayerSM.States;

namespace Meta.Upgrades.UpgradeSkills
{
    public class StrongPunch : Upgrade
    {
        private readonly List<(float, float)> grades = new()
        {
            (3.5f, 2f),
            (2.5f, 2.5f),
            (2f, 3f)
        };
        
        public (float, float) curGrade;
        
        private void LoadGrades()
        {
            curGrade = grades[Level - 1];
        }
        
        public override void Action(PlayerSM playerSm, SocketViewInHUD socketViewInHUD)
        {
            base.Activate(playerSm);
            LoadGrades();
            playerSm.StrongPunchState.socketViewInHUD = socketViewInHUD;
            playerSm.ChangeState(playerSm.StrongPunchState);

        }
    }
}