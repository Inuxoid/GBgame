using System;
using System.Collections.Generic;
using System.Linq;

namespace Meta.Upgrades
{
    public class UpgradeShop : IUpgradeShopActions
    {
        private List<BaseUpgrade> upgrades;

        public UpgradeShop()
        {
            upgrades = new List<BaseUpgrade>();
        }

        public bool Buy(BaseUpgrade upgrade)
        {
            upgrades.Find(e => e == upgrade).isBought = true;
            return true;
        }

        public bool Upgrade(BaseUpgrade upgrade)
        {
            if (!upgrades.Find(e => e == upgrade).isBought) return false;
            upgrade.level++;
            return true;
        }

        public bool IsUpgradeCanBeBought(BaseUpgrade upgrade)
        {
            return upgrade.requirements.All(e=>upgrades.Any(baseUpgrade=>upgrade.isBought));
        }
    }
}
