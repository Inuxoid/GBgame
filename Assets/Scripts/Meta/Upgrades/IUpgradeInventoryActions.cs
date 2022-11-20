using System.Net.Sockets;

namespace Meta.Upgrades
{
    public interface IUpgradeInventoryActions
    {
        bool SetUpgrade(BaseUpgrade upgrade);
        bool UnsetUpgrade(BaseSocket socket);
        bool IsUpgradeCanBeUsed(BaseUpgrade upgrade);
        bool IsUpgradeIsReady(BaseUpgrade upgrade); // Idk mb check activity of sockets
    }
}