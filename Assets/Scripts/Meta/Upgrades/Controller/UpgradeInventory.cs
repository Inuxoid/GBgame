using System.Collections.Generic;
using System.Linq;

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
                new Socket() {ID = 1, Upgrade = null, IsUltimate = false},
                new Socket() {ID = 2, Upgrade = null, IsUltimate = false},
                new Socket() {ID = 3, Upgrade = null, IsUltimate = false},
                new Socket() {ID = 4, Upgrade = null, IsUltimate = true}
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
            throw new System.NotImplementedException();
        }

        public bool IsUpgradeIsReady(Upgrade upgrade)
        {
            throw new System.NotImplementedException();
        }

        public Socket IsUpgradeInSocket(Upgrade upgrade)
        {
            return Sockets.FirstOrDefault(socket => socket.Upgrade == upgrade);
        }
    }
}