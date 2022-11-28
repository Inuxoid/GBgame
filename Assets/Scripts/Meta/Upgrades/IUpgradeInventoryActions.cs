using System.Net.Sockets;

namespace Meta.Upgrades
{
    public interface IUpgradeInventoryActions
    {
        bool SetUpgrade(BaseUpgrade upgrade);
        bool UnsetUpgrade(BaseUpgrade upgrade);
        bool IsUpgradeCanBeUsed(BaseUpgrade upgrade);
        bool IsUpgradeIsReady(BaseUpgrade upgrade); // Idk mb check activity of sockets
        BaseSocket IsUpgradeInSocket(BaseUpgrade upgrade);
    }
}