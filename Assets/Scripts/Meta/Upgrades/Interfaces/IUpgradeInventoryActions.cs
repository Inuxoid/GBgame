using Meta.Upgrades.Controller;
using Socket = Meta.Upgrades.Controller.Socket;

namespace Meta.Upgrades.Interfaces
{
    public interface IUpgradeInventoryActions
    {
        bool SetUpgrade(Upgrade upgrade);
        bool UnsetUpgrade(Upgrade upgrade);
        bool IsUpgradeCanBeUsed(Upgrade upgrade);
        bool IsUpgradeIsReady(Upgrade upgrade); // Idk mb check activity of sockets
        Socket IsUpgradeInSocket(Upgrade upgrade);
    }
}