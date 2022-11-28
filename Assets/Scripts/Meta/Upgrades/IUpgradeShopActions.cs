using System.Collections.Generic;
namespace Meta.Upgrades
{
    public interface IUpgradeShopActions
    {
        bool Buy(BaseUpgrade upgrade);
        bool Upgrade(BaseUpgrade upgrade); // Yeah, we are upgrading an upgrade
        bool IsUpgradeCanBeBought(BaseUpgrade upgrade);
        IEnumerable<BaseUpgrade> GetUpgrades();
    }
}