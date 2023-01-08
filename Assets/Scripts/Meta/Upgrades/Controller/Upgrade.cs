using System;
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
    }
}
