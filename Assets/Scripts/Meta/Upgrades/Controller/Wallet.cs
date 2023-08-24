using Dto;

namespace Meta.Upgrades.Controller
{
    public class Wallet
    {
        private static Wallet _instance;
        private int balance = 3000;

        public int Balance
        {
            get => balance;
            set
            {
                balance = value;
            }
        }

        public static Wallet GetInstance()
        {
            return _instance ??= new Wallet();
        }
    }
}