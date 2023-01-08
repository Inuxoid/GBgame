using Meta.Upgrades.Controller;

namespace Meta.Upgrades
{
    public static class UpgradeLib
    {
        public static Upgrade Upgrade1 = new Upgrade() {ID = 1, Level = 1, Name = "ConUP", Requirements = null, Ultimate = false, IconPath = "SkillIcons/ConUP", UpgradeState = Upgrade.UpgradeStates.InShop, UpgradeType = Upgrade.UpgradeTypes.Common, Description = "You got over countless battles, your survivability is increased.", BuyCost = 10};
        public static Upgrade Upgrade2 = new Upgrade() {ID = 2, Level = 1, Name = "Invisibility", Requirements = null, Ultimate = false, IconPath = "SkillIcons/Invisibility", UpgradeState = Upgrade.UpgradeStates.InShop, UpgradeType = Upgrade.UpgradeTypes.Operative, Description = "The ability to perform work quietly and imperceptibly is the best quality of an operative.", BuyCost = 20};
        public static Upgrade Upgrade3 = new Upgrade() {ID = 3, Level = 1, Name = "Berserk Smash", Requirements = null, Ultimate = false, IconPath = "SkillIcons/BerserkSmash", UpgradeState = Upgrade.UpgradeStates.InShop, UpgradeType = Upgrade.UpgradeTypes.Gladiator, Description = "A blow that brought you many victories in the gladiatorial arena.", BuyCost = 30};
    }
}