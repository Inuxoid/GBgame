using System;
using System.Collections.Generic;
using System.Linq;

namespace Meta.Upgrades
{
    public class UpgradeShop : IUpgradeShopActions, IUpgradeInventoryActions
    {
        private List<BaseUpgrade> upgrades;
        private List<BaseSocket> sockets;
        private static UpgradeShop _instance;

        private UpgradeShop()
        {
            upgrades = new List<BaseUpgrade>() {UpgradeLib.Upgrade1, UpgradeLib.Upgrade2, UpgradeLib.Upgrade3};
            sockets = new List<BaseSocket>();
        }
 
        public static UpgradeShop GetInstance() => _instance ??= new UpgradeShop();

        public bool Buy(BaseUpgrade upgrade)
        {
            if (upgrades.Find(e => e == upgrade) == null) return false;
            upgrades.Find(e => e == upgrade).isBought = true;
            return true;
        }

        public bool Upgrade(BaseUpgrade upgrade)
        {
            if (upgrades.Find(e => e == upgrade) == null) return false;
            if (!upgrades.Find(e => e == upgrade).isBought) return false;
            upgrade.level++;
            return true;
        }

        public bool IsUpgradeCanBeBought(BaseUpgrade upgrade)
        {
            return upgrade.requirements.All(e=>upgrades.Any(baseUpgrade=>upgrade.isBought));
        }

        public IEnumerable<BaseUpgrade> GetUpgrades()
        {
            return upgrades;
        }

        public bool SetUpgrade(BaseUpgrade upgrade)
        {
            if (sockets.All(e => e.upgrade != null)) return false;
            if (upgrades.Find(e => e == upgrade) == null) return false;
            sockets.First(e => e.upgrade == null).upgrade = upgrade;
            upgrade.isSelected = true;
            return true;
        }

        public bool UnsetUpgrade(BaseUpgrade upgrade)
        {
            if (IsUpgradeInSocket(upgrade) == null) return false;
            if (upgrades.Find(e => e == upgrade) == null) return false;
            IsUpgradeInSocket(upgrade).upgrade = null;
            upgrade.isSelected = false;
            return true;
        }

        public bool IsUpgradeCanBeUsed(BaseUpgrade upgrade)
        {
            throw new System.NotImplementedException();
        }

        public bool IsUpgradeIsReady(BaseUpgrade upgrade)
        {
            throw new System.NotImplementedException();
        }

        public BaseSocket IsUpgradeInSocket(BaseUpgrade upgrade)
        {
            return sockets.FirstOrDefault(socket => socket.upgrade == upgrade);
        }
    }
}
