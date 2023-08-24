using System.Collections.Generic;
using System.Linq;
using Meta.Upgrades.Interfaces;
using Meta.Upgrades.Model;

namespace Meta.Upgrades.Controller
{
    public class UpgradeShop : IUpgradeShopActions
    {
        private static UpgradeShop _instance;
        
        public List<Upgrade> Upgrades { get; set; }

        private UpgradeShop()
        {
            Upgrades = new List<Upgrade>()
            {
                UpgradeLib.StrongPunch, 
                UpgradeLib.HyperphasicKatana, 
                UpgradeLib.SwordMaster, 
                UpgradeLib.Nanomachines,
                UpgradeLib.Fury, 
                UpgradeLib.Upgrade6, 
                UpgradeLib.Upgrade7, 
                UpgradeLib.Upgrade8,
                UpgradeLib.Upgrade9,
                UpgradeLib.Upgrade10,
            };
        }
 
        public static UpgradeShop GetInstance() => _instance ??= new UpgradeShop();

        public bool Buy(Upgrade upgrade)
        {
            if (!IsUpgradeCanBeBought(upgrade)) return false;
            upgrade.UpgradeState = Controller.Upgrade.UpgradeStates.InInventory;
            Wallet.GetInstance().Balance -= upgrade.BuyCost;
            return true;
        }

        public bool Upgrade(Upgrade upgrade)
        {
            if (!IsUpgradeCanBeUpgraded(upgrade)) return false;
            Wallet.GetInstance().Balance -= upgrade.UpgCost[upgrade.Level];
            upgrade.Level++;
            return true;
        }

        public bool IsUpgradeCanBeBought(Upgrade upgrade)
        {
            if (upgrade == null) return false;
            if (upgrade.UpgradeState != Controller.Upgrade.UpgradeStates.InShop) return false;
            if (upgrade.Requirements == null) return true;
            return upgrade.Requirements.All(e => Upgrades.Any(baseUpgrade =>
                upgrade.UpgradeState is Controller.Upgrade.UpgradeStates.InSocket
                    or Controller.Upgrade.UpgradeStates.InInventory));
        }
        
        public bool IsUpgradeCanBeUpgraded(Upgrade upgrade)
        {
            if (upgrade == null) return false;
            if (upgrade.UpgradeState is Controller.Upgrade.UpgradeStates.InShop) return false;
            if (upgrade.Level >= 3) return false;
            return upgrade.UpgCost[upgrade.Level] <= Wallet.GetInstance().Balance;
        }
    }
}
