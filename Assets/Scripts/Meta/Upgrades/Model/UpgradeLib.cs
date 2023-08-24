using System.Collections.Generic;
using Meta.Upgrades.Controller;
using Meta.Upgrades.UpgradeSkills;

namespace Meta.Upgrades.Model
{
    public static class UpgradeLib
    {
        public static StrongPunch StrongPunch = new StrongPunch()
        {
            ID = 1,
            Level = 1,
            Name = "Strong punch",
            Requirements = null,
            Ultimate = false,
            IconPath = "SkillIcons/smash",
            UpgradeState = Upgrade.UpgradeStates.InShop,
            UpgradeType = Upgrade.UpgradeTypes.Gladiator,
            Description = "Strong strike. Description.",
            BuyCost = 100, UpgCost = new List<int>() { 0, 350, 500 },
            CanBeUsed = true,
            IsReady = true
        };

        public static HyperphasicKatana HyperphasicKatana = new HyperphasicKatana()
        {
            ID = 2,
            Level = 1,
            Name = "Hyper Katana",
            Requirements = null,
            Ultimate = false,
            IconPath = "SkillIcons/katana",
            UpgradeState = Upgrade.UpgradeStates.InShop,
            UpgradeType = Upgrade.UpgradeTypes.Gladiator,
            Description = "Hyper Katana. Description.",
            BuyCost = 200,
            UpgCost = new List<int>() { 0, 350, 500 },
            CanBeUsed = false,
            IsReady = false
        };

        public static SwordMaster SwordMaster = new SwordMaster()
        {
            ID = 3,
            Level = 1,
            Name = "Sword Master",
            Requirements = null,
            Ultimate = false,
            IconPath = "SkillIcons/swordmaster",
            UpgradeState = Upgrade.UpgradeStates.InShop,
            UpgradeType = Upgrade.UpgradeTypes.Gladiator,
            Description = "Sword Master. Description.",
            BuyCost = 100,
            UpgCost = new List<int>() { 0, 350, 500 },
            CanBeUsed = false,
            IsReady = false
        };

        public static Nanomachines Nanomachines = new Nanomachines()
        {
            ID = 4,
            Level = 1,
            Name = "Nanomachines",
            Requirements = null,
            Ultimate = false,
            IconPath = "SkillIcons/nanomachines",
            UpgradeState = Upgrade.UpgradeStates.InShop,
            UpgradeType = Upgrade.UpgradeTypes.Gladiator,
            Description = "Nanomachines. Description.",
            BuyCost = 200,
            UpgCost = new List<int>() { 0, 250, 500 },
            CanBeUsed = false,
            IsReady = false
        };

        public static Fury Fury = new Fury()
        {
            ID = 5,
            Level = 1,
            Name = "Berserker",
            Requirements = null,
            Ultimate = true,
            IconPath = "SkillIcons/berserk",
            UpgradeState = Upgrade.UpgradeStates.InShop,
            UpgradeType = Upgrade.UpgradeTypes.Gladiator,
            Description = "ULT: Berserker. Description.",
            BuyCost = 350,
            UpgCost = new List<int>() { 0, 350, 500 },
            CanBeUsed = true,
            IsReady = true
        };

        public static Upgrade Upgrade6 = new Upgrade()
        {
            ID = 6,
            Level = 1,
            Name = "Stealth Kill",
            Requirements = null,
            Ultimate = false,
            IconPath = "SkillIcons/StealthKill",
            UpgradeState = Upgrade.UpgradeStates.InShop,
            UpgradeType = Upgrade.UpgradeTypes.Operative,
            Description = "Stealth Kill. Description.",
            BuyCost = 100,
            UpgCost = new List<int>() { 0, 350, 500 }
        };

        public static Upgrade Upgrade7 = new Upgrade()
        {
            ID = 7,
            Level = 1,
            Name = "Roll",
            Requirements = null,
            Ultimate = false,
            IconPath = "SkillIcons/Roll",
            UpgradeState = Upgrade.UpgradeStates.InShop,
            UpgradeType = Upgrade.UpgradeTypes.Operative,
            Description = "Roll. Description.",
            BuyCost = 200,
            UpgCost = new List<int>() { 0, 250, 500 }
        };

        public static Upgrade Upgrade8 = new Upgrade()
        {
            ID = 8,
            Level = 1,
            Name = "Servomotors",
            Requirements = null,
            Ultimate = false,
            IconPath = "SkillIcons/Servomotors",
            UpgradeState = Upgrade.UpgradeStates.InShop,
            UpgradeType = Upgrade.UpgradeTypes.Operative,
            Description = "Servomotors. Description.",
            BuyCost = 200,
            UpgCost = new List<int>() { 0, 250, 500 }
        };

        public static Upgrade Upgrade9 = new Upgrade()
        {
            ID = 9,
            Level = 1,
            Name = "Shield Generator",
            Requirements = null,
            Ultimate = false,
            IconPath = "SkillIcons/ShieldGenerator",
            UpgradeState = Upgrade.UpgradeStates.InShop,
            UpgradeType = Upgrade.UpgradeTypes.Operative,
            Description = "Shield Generator. Description.",
            BuyCost = 200,
            UpgCost = new List<int>() { 0, 350, 500 }
        };

        public static Upgrade Upgrade10 = new Upgrade()
        {
            ID = 10,
            Level = 1,
            Name = "Stealth Module",
            Requirements = null,
            Ultimate = true,
            IconPath = "SkillIcons/StealthModule",
            UpgradeState = Upgrade.UpgradeStates.InShop,
            UpgradeType = Upgrade.UpgradeTypes.Operative,
            Description = "ULT: Stealth Module. Description.",
            BuyCost = 350,
            UpgCost = new List<int>() { 0, 350, 500 }
        };
    }
}