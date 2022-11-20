using System;

namespace Meta.Upgrades
{
    [Serializable]
    public class BaseSocket
    {
        public int id;
        public BaseUpgrade upgrade;
        public bool isUltimate;
    }
}