using System.Collections.Generic;
using System.Linq;

namespace Meta.Upgrades
{
    public class UpgradeInventory : IUpgradeInventoryActions
    {
        private List<BaseSocket> sockets;

        public UpgradeInventory()
        {
            sockets = new List<BaseSocket>();
        }

        public bool SetUpgrade(BaseUpgrade upgrade)
        {
            if (sockets.All(e => e.upgrade != null)) return false;
            {
                sockets.First(e => e.upgrade == null).upgrade = upgrade;
                return true;
            }

        }

        public bool UnsetUpgrade(BaseSocket socket)
        {
            socket.upgrade = null;
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
    }
}