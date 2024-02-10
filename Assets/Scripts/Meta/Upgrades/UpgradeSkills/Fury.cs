using System;
using System.Collections.Generic;
using Meta.Upgrades.Controller;
using Meta.Upgrades.Model;
using StateMachines.PlayerSM;
using System.Threading;
using Meta.Upgrades.View.HUD;
using UnityEngine;

namespace Meta.Upgrades.UpgradeSkills
{
    public class Fury : Upgrade
    {
        private readonly List<(float, float, float, float)> grades = new()
        {
            (1.5f, 5f, 45f, 15f),
            (2f, 1.5f, 40f, 15f),
            (2.5f, 30f, 30f, 15f)
        };

        public (float, float, float, float) curGrade;
        private System.Threading.Timer cooldownTimer;
        private System.Threading.Timer endTimer;
        private System.Threading.Timer durationTimer;

        private void LoadGrades()
        {
            curGrade = grades[Level - 1];
        }

        public override void Action(PlayerSM playerSm, SocketViewInHUD socketViewInHUD)
        {
            base.Activate(playerSm);
            LoadGrades();
            Debug.Log("Timers started1");
            if (!IsReady) return;
            Debug.Log("Timers started2");
            IsReady = false;
            playerSm.damageMult = curGrade.Item1;


            // Запуск таймера для задержки (cooldown)
            cooldownTimer = new System.Threading.Timer(CooldownTimerCallback, null, (int)(curGrade.Item3 * 1000),
                Timeout.Infinite);
            
            // Запуск таймера для завершения
            endTimer = new System.Threading.Timer(FinishTimerCallback, playerSm, (int)(curGrade.Item4 * 1000),
                Timeout.Infinite);

            // Запуск таймера для тиков
            durationTimer = new System.Threading.Timer(DurationTimerCallback, playerSm, (int)curGrade.Item4 * 1000, 1000);
            
        }

        private void CooldownTimerCallback(object state)
        {
            IsReady = true;
            Debug.Log("Ready");
        }

        private void DurationTimerCallback(object state)
        {
            PlayerSM playerSm = (PlayerSM)state;
            playerSm.liveCycle.Heal(curGrade.Item2);
            Debug.Log("Tik");
        }

        private void FinishTimerCallback(object state)
        {
            durationTimer.Dispose();
            PlayerSM playerSm = (PlayerSM)state;
            playerSm.damageMult = 1;
            Debug.Log("Finish");
        }
    }
}
