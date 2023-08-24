using System;
using UnityEngine;

namespace Meta.Upgrades.Controller
{
    [Serializable]
    public class Socket
    {
        [SerializeField]
        private int id;
        [SerializeField]
        private Upgrade upgrade;
        [SerializeField]
        private bool isUltimate;

        public bool IsUltimate
        {
            get => isUltimate;
            set => isUltimate = value;
        }

        public Upgrade Upgrade
        {
            get => upgrade;
            set => upgrade = value;
        }

        public int ID
        {
            get => id;
            set => id = value;
        }
    }
}