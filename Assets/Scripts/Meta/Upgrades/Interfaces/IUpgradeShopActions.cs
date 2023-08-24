using Meta.Upgrades.Controller;

namespace Meta.Upgrades.Interfaces
{
    public interface IUpgradeShopActions
    {
        bool Buy(Upgrade upgrade);
        bool Upgrade(Upgrade upgrade); // Yeah, we are upgrading an upgrade
        bool IsUpgradeCanBeBought(Upgrade upgrade);
    }
}