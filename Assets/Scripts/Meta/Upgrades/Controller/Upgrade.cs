using System;
using System.Collections.Generic;
using Meta.Upgrades.View.HUD;
using StateMachines.PlayerSM;
using UnityEngine;

namespace Meta.Upgrades.Controller
{
    [Serializable]
    public class Upgrade
    {
        [SerializeField]
        private int id;
        [SerializeField]
        private string name;
        [SerializeField]
        private string iconPath;
        public enum UpgradeStates {InShop, InInventory, InSocket}
        private UpgradeStates upgradeState;
        public enum UpgradeTypes {Common, Gladiator, Operative}
        private UpgradeTypes upgradeTypes;
        private int level;
        private Upgrade[] requirements;
        private bool ultimate;
        private string description;
        private int buyCost;
        private List<int> upgCost;
        private bool canBeUsed;
        private bool isReady;

        public bool CanBeUsed
        {
            get => canBeUsed;
            set => canBeUsed = value;
        }

        public bool IsReady
        {
            get => isReady;
            set => isReady = value;
        }

        public List<int> UpgCost
        {
            get => upgCost;
            set => upgCost = value;
        }

        public int BuyCost
        {
            get => buyCost;
            set => buyCost = value;
        }

        public UpgradeStates UpgradeState
        {
            get => upgradeState;
            set => upgradeState = value;
        }
        
        public UpgradeTypes UpgradeType
        {
            get => upgradeTypes;
            set => upgradeTypes = value;
        }
        
        public string Description
        {
            get => description;
            set => description = value;
        }
        
        public bool Ultimate
        {
            get => ultimate;
            set => ultimate = value;
        }

        public Upgrade[] Requirements
        {
            get => requirements;
            set => requirements = value;
        }

        public int Level
        {
            get => level;
            set => level = value;
        }

        public string IconPath
        {
            get => iconPath;
            set => iconPath = value;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public int ID
        {
            get => id;
            set => id = value;
        }

        public virtual void Action(PlayerSM playerSm, SocketViewInHUD socketViewInHUD)
        {
            
        }

        public virtual void Activate(PlayerSM playerSm)
        {
            
        }

        public virtual void Deactivate(PlayerSM playerSm)
        {
            
        }
    }
}
