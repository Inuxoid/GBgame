using System;

namespace Meta.Upgrades
{
    [Serializable]
    public class BaseUpgrade
    {
        public int id;
        public string name;
        public string iconPath;
        public bool isBought;
        public int level;
        public BaseUpgrade[] requirements;
        public bool ultimate;
    }
}
