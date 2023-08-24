using System.Collections.Generic;
using System.Linq;
using Meta.Upgrades.Interfaces;

namespace Meta.Upgrades.Controller
{
    public class UpgradeInventory : IUpgradeInventoryActions
    {
        private static UpgradeInventory _instance;
        public static UpgradeInventory GetInstance() => _instance ??= new UpgradeInventory();
        
        public List<Socket> Sockets { get; set; }

        private UpgradeInventory()
        {
            Sockets = new List<Socket>()
            {
                new() {ID = 1, Upgrade = null, IsUltimate = false},
                new() {ID = 2, Upgrade = null, IsUltimate = false},
                new() {ID = 3, Upgrade = null, IsUltimate = false},
                new() {ID = 4, Upgrade = null, IsUltimate = true}
            };
        }
        
        public bool SetUpgrade(Upgrade upgrade)
        {
            if (Sockets.All(e => e.Upgrade != null)) return false;
            if (UpgradeShop.GetInstance().Upgrades.Find(e => e == upgrade) == null) return false;
            Sockets.First(e => e.Upgrade == null).Upgrade = upgrade;
            upgrade.UpgradeState = Upgrade.UpgradeStates.InSocket;
            return true;
        }

        public bool UnsetUpgrade(Upgrade upgrade)
        {
            if (IsUpgradeInSocket(upgrade) == null) return false;
            if (UpgradeShop.GetInstance().Upgrades.Find(e => e == upgrade) == null) return false;
            IsUpgradeInSocket(upgrade).Upgrade = null;
            upgrade.UpgradeState = Upgrade.UpgradeStates.InInventory;
            return true;
        }

        public bool IsUpgradeCanBeUsed(Upgrade upgrade)
        {
            return upgrade.CanBeUsed;
        }

        public bool IsUpgradeIsReady(Upgrade upgrade)
        {
            return upgrade.IsReady;
        }

        public Socket IsUpgradeInSocket(Upgrade upgrade)
        {
            return Sockets.FirstOrDefault(socket => socket.Upgrade == upgrade);
        }
    }
}