using System;
using System.Collections.Generic;
using System.Linq;
using Meta.Upgrades.Controller;
using UnityEngine;

namespace Meta.Upgrades
{
    public class UpgradeShop : IUpgradeShopActions
    {
        private static UpgradeShop _instance;
        
        public List<Upgrade> Upgrades { get; set; }

        private UpgradeShop()
        {
            Upgrades = new List<Upgrade>() {UpgradeLib.Upgrade1, UpgradeLib.Upgrade2, UpgradeLib.Upgrade3};
        }
 
        public static UpgradeShop GetInstance() => _instance ??= new UpgradeShop();

        public bool Buy(Upgrade upgrade)
        {
            if (Upgrades.Find(e => e == upgrade) == null) return false;
            Upgrades.Find(e => e == upgrade).UpgradeState = Controller.Upgrade.UpgradeStates.InInventory;
            return true;
        }

        public bool Upgrade(Upgrade upgrade)
        {
            if (Upgrades.Find(e => e == upgrade).Level >= 3) return false;
            if (Upgrades.Find(e => e == upgrade) == null) return false;
            if (Upgrades.Find(e => e == upgrade).UpgradeState is not (Controller.Upgrade.UpgradeStates.InSocket
                or Controller.Upgrade.UpgradeStates.InInventory)) return false;
            upgrade.Level++;
            return true;
        }

        public bool IsUpgradeCanBeBought(Upgrade upgrade)
        {
            return upgrade.Requirements.All(e => Upgrades.Any(baseUpgrade =>
                upgrade.UpgradeState is Controller.Upgrade.UpgradeStates.InSocket
                    or Controller.Upgrade.UpgradeStates.InInventory));
        }
    }
}
